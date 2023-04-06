using FunctionApp.HttpClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using functionApp.services;
using Shared.Models;
using Shared.Services;
using System.Threading;

namespace FunctionApp;

public class StoreWeatherData
{
    private readonly IOpenWeatherHttpClient _openWeatherHttpClient;
    private readonly IClock _clock;
    private readonly IWeatherDataRepository _weatherDataRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ILogger _logger;

    public StoreWeatherData(ILoggerFactory loggerFactory, IOpenWeatherHttpClient openWeatherHttpClient, IClock clock, IWeatherDataRepository weatherDataRepository, ICityRepository cityRepository)
    {
        _openWeatherHttpClient = openWeatherHttpClient;
        _clock = clock;
        _weatherDataRepository = weatherDataRepository;
        _cityRepository = cityRepository;
        _logger = loggerFactory.CreateLogger<StoreWeatherData>();
    }

    [Function("StoreWeatherData")]
    public async Task Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerTriggerInfo myTimer, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {_clock.DateTimeNow}");

        var cities = await _cityRepository.GetAllCities(cancellationToken);
        await Task.WhenAll(cities.Select(city => SaveWeatherForecastForCity(city, cancellationToken)).ToList());
    }

    private async Task SaveWeatherForecastForCity(CityInfo city, CancellationToken cancellationToken)
    {
        var weatherForecast = await _openWeatherHttpClient.FetchWeatherData(city.lat, city.lon, cancellationToken);
        if (weatherForecast is null)
        {
            _logger.LogError("Error parsing weather forecast");
            return;
        }

        WeatherInfo weatherInfo = new()
        {
            Country = city.Country,
            City = city.Name,
            ForecastTime = _clock.DateTimeOffsetNow,
            PartitionKey = $"{city.Name}@{city.Country}",
            TemperatureInCelsius = weatherForecast.main.temp,
            WindSpeed = weatherForecast.wind.speed
        };

        await _weatherDataRepository.StoreWeatherForecast(weatherInfo, city, cancellationToken);
    }
}
