using Shared.Models;

namespace Shared.Services;

public interface IWeatherDataRepository
{
    Task StoreWeatherForecast(WeatherInfo weatherInfo, CancellationToken cancellationToken = default);
}
