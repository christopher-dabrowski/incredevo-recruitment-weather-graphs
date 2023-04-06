using FunctionApp.HttpClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FunctionApp;

public class StoreWeatherData
{
    private readonly IOpenWeatherHttpClient _openWeatherHttpClient;
    private readonly ILogger _logger;

    public StoreWeatherData(ILoggerFactory loggerFactory, IOpenWeatherHttpClient openWeatherHttpClient)
    {
        _openWeatherHttpClient = openWeatherHttpClient;
        _logger = loggerFactory.CreateLogger<StoreWeatherData>();
    }

    [Function("StoreWeatherData")]
    public async Task Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerTriggerInfo myTimer, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        var weatherForecast = await _openWeatherHttpClient.FetchWeatherData("52.2337172", "21.071432235636493", cancellationToken);
        _logger.LogInformation(JsonSerializer.Serialize(weatherForecast));
    }
}
