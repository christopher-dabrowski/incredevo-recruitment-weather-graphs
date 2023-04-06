using FunctionApp.Models;

namespace FunctionApp.HttpClients;

public interface IOpenWeatherHttpClient
{
    Task<WeatherResponse?> FetchWeatherData(string lat, string lon, CancellationToken cancellationToken);
}
