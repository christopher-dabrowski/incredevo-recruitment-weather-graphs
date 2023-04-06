using System.Text.Json;
using FunctionApp.HttpClients;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public class StoreWeatherData
    {
        private readonly IOpenWeatherHttpClient _openWeatherHttpClient;

        public StoreWeatherData(IOpenWeatherHttpClient openWeatherHttpClient)
        {
            _openWeatherHttpClient = openWeatherHttpClient;
        }

        [FunctionName("StoreWeatherData")]
        public async Task Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log, CancellationToken cancellationToken)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var weatherForecast = await _openWeatherHttpClient.FetchWeatherData("52.2337172", "21.071432235636493", cancellationToken);
            log.LogInformation(JsonSerializer.Serialize(weatherForecast));
        }
    }
}
