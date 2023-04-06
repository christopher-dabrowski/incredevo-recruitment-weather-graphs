using Azure.Data.Tables;
using functionApp.services;
using Microsoft.Extensions.Configuration;
using Shared.Models;

namespace Shared.Services;

public class WeatherDataRepository : IWeatherDataRepository
{
    private readonly IClock _clock;
    private readonly TableClient _weatherDataTableClient;
    private readonly TableClient _cityCurrentWeatherTableClient;

    public WeatherDataRepository(IConfiguration configuration, IClock clock)
    {
        _clock = clock;
        _weatherDataTableClient = new TableClient(configuration[Config.WeatherDataStorageConnectionConfigName], Config.WeatherDataTableName);
        _cityCurrentWeatherTableClient = new TableClient(configuration[Config.WeatherDataStorageConnectionConfigName], Config.CityCurrentWeatherTableName);
    }

    public async Task StoreWeatherForecast(WeatherInfo weatherInfo, CityInfo city, CancellationToken cancellationToken = default)
    {
        await _weatherDataTableClient.CreateIfNotExistsAsync(cancellationToken);
        await _weatherDataTableClient.AddEntityAsync(weatherInfo, cancellationToken);

        await _cityCurrentWeatherTableClient.CreateIfNotExistsAsync(cancellationToken);

        var currentCityWeather = new CityCurrentWeatherInfo
        {
            PartitionKey = city.Country,
            RowKey = city.Name,
            lat = city.lat,
            lon = city.lon,
            TemperatureInCelsius = weatherInfo.TemperatureInCelsius,
            WindSpeed = weatherInfo.WindSpeed,
        };

        await _cityCurrentWeatherTableClient.UpsertEntityAsync(currentCityWeather, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<WeatherInfo>> GetAllWeatherForecasts(DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken cancellationToken = default)
    {
        from ??= _clock.DateTimeOffsetNow.AddDays(-1);
        to ??= _clock.DateTimeOffsetNow;

        return await _weatherDataTableClient.QueryAsync<WeatherInfo>(
            weatherInfo => weatherInfo.ForecastTime >= from && weatherInfo.ForecastTime < to, cancellationToken: cancellationToken).ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<WeatherInfo>> GetWeatherInCity(string cityName, DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken cancellationToken = default)
    {
        from ??= _clock.DateTimeOffsetNow.AddHours(-1);
        to ??= _clock.DateTimeOffsetNow;

        return await _weatherDataTableClient.QueryAsync<WeatherInfo>(
            weatherInfo => weatherInfo.City == cityName && weatherInfo.ForecastTime >= from && weatherInfo.ForecastTime < to, cancellationToken: cancellationToken).ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<CityCurrentWeatherInfo>> GetCurrentWeatherForAllCities(
        CancellationToken cancellationToken = default) =>
        await _cityCurrentWeatherTableClient.QueryAsync<CityCurrentWeatherInfo>().ToListAsync(cancellationToken);
}
