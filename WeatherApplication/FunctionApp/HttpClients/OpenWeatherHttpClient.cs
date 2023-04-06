using System.Net.Http.Json;
using FunctionApp.Models;
using Microsoft.Extensions.Configuration;

namespace FunctionApp.HttpClients;

public class OpenWeatherHttpClient : IOpenWeatherHttpClient
{
    private const string ApiVersion = "2.5";
    private const string UnitType = "metric";

    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.openweathermap.org");

        _apiKey = configuration["OpenWeatherApiKey"];
    }

    public async Task<WeatherResponse?> FetchWeatherData(string lat, string lon, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<WeatherResponse>($"/data/{ApiVersion}/weather?lat={lat}&lon={lon}&appid={_apiKey}&units={UnitType}", cancellationToken);
    }
}
