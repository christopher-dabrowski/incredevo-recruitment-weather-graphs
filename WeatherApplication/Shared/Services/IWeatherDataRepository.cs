using Shared.Models;

namespace Shared.Services;

public interface IWeatherDataRepository
{
    Task StoreWeatherForecast(WeatherInfo weatherInfo, CityInfo city, CancellationToken cancellationToken = default);

    Task<IEnumerable<WeatherInfo>> GetAllWeatherForecasts(DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CityCurrentWeatherInfo>> GetCurrentWeatherForAllCities(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<WeatherInfo>> GetWeatherInCity(string cityName, DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);
}
