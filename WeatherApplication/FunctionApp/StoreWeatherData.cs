using FunctionApp.HttpClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using functionApp.services;
using Shared.Models;
using Shared.Services;

namespace FunctionApp;

public class StoreWeatherData
{
    private readonly IOpenWeatherHttpClient _openWeatherHttpClient;
    private readonly IClock _clock;
    private readonly IWeatherDataRepository _weatherDataRepository;
    private readonly ILogger _logger;

    public StoreWeatherData(ILoggerFactory loggerFactory, IOpenWeatherHttpClient openWeatherHttpClient, IClock clock, IWeatherDataRepository weatherDataRepository)
    {
        _openWeatherHttpClient = openWeatherHttpClient;
        _clock = clock;
        _weatherDataRepository = weatherDataRepository;
        _logger = loggerFactory.CreateLogger<StoreWeatherData>();
    }

    [Function("StoreWeatherData")]
    public async Task Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerTriggerInfo myTimer, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {_clock.DateTimeNow}");

        var weatherForecast = await _openWeatherHttpClient.FetchWeatherData("52.2337172", "21.071432235636493", cancellationToken);
        if (weatherForecast is null)
        {
            _logger.LogError("Error parsing weather forecast");
            return;
        }

        WeatherInfo weatherInfo = new()
        {
            Country = "Poland",
            City = "Warsaw",
            ForecastTime = _clock.DateTimeOffsetNow,
            PartitionKey = "Warsaw@Poland",
            TemperatureInCelsius = weatherForecast.main.temp,
            WindSpeed = weatherForecast.wind.speed
        };

        await _weatherDataRepository.StoreWeatherForecast(weatherInfo, cancellationToken);
    }
}
