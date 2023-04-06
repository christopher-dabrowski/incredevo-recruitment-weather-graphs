using Shared.Models;

namespace Shared.Services;

public interface ICityRepository
{
    Task EnsureCityTableExists(CancellationToken cancellationToken = default);

    Task<IEnumerable<CityInfo>> GetAllCities(CancellationToken cancellationToken = default);
}
