using Azure.Data.Tables;
using functionApp.services;
using Microsoft.Extensions.Configuration;
using Shared.Models;

namespace Shared.Services;

public class WeatherDataRepository : IWeatherDataRepository
{
    private readonly IClock _clock;
    private readonly TableClient _tableClient;

    public WeatherDataRepository(IConfiguration configuration, IClock clock)
    {
        _clock = clock;
        _tableClient = new TableClient(configuration[Config.WeatherDataStorageConnectionConfigName], Config.WeatherDataTableName);
    }

    public async Task StoreWeatherForecast(WeatherInfo weatherInfo, CancellationToken cancellationToken = default)
    {
        await _tableClient.CreateIfNotExistsAsync(cancellationToken);
        await _tableClient.AddEntityAsync(weatherInfo, cancellationToken);
    }

    public async Task<IEnumerable<WeatherInfo>> GetAllWeatherForecasts(DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken cancellationToken = default)
    {
        from ??= _clock.DateTimeOffsetNow.AddDays(-1);
        to ??= _clock.DateTimeOffsetNow;

        return await _tableClient.QueryAsync<WeatherInfo>(
            weatherInfo => weatherInfo.ForecastTime >= from && weatherInfo.ForecastTime < to, cancellationToken: cancellationToken).ToArrayAsync(cancellationToken);
    }
}
