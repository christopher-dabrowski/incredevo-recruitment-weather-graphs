using Shared.Models;

namespace Shared.Services;

public interface IWeatherDataRepository
{
    Task StoreWeatherForecast(WeatherInfo weatherInfo, CancellationToken cancellationToken = default);

    Task<IEnumerable<WeatherInfo>> GetAllWeatherForecasts(DateTimeOffset? from = null, DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);
}
