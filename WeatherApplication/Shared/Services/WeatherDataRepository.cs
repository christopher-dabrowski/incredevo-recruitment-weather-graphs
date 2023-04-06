using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Shared.Models;

namespace Shared.Services;

public class WeatherDataRepository : IWeatherDataRepository
{
    private readonly TableClient _tableClient;

    public WeatherDataRepository(IConfiguration configuration)
    {
        _tableClient = new TableClient(configuration[Config.WeatherDataStorageConnectionConfigName], Config.WeatherDataTableName);
    }

    public async Task StoreWeatherForecast(WeatherInfo weatherInfo, CancellationToken cancellationToken = default)
    {
        await _tableClient.CreateIfNotExistsAsync(cancellationToken);
        await _tableClient.AddEntityAsync(weatherInfo, cancellationToken);
    }
}
