using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Shared.Models;

namespace Shared.Services;

public class CityRepository
{
    private readonly TableServiceClient _tableServiceClient;
    private readonly TableClient _tableClient;

    public CityRepository(IConfiguration configuration)
    {
        _tableServiceClient = new TableServiceClient(configuration[Config.WeatherDataStorageConnectionConfigName]);
        _tableClient = new TableClient(configuration[Config.WeatherDataStorageConnectionConfigName], Config.CityTableName);
    }

    public async Task EnsureCityTableExists(CancellationToken cancellationToken = default)
    {
        var cityTables = await _tableServiceClient.QueryAsync(table => table.Name == Config.CityTableName).ToListAsync(cancellationToken);
        if (cityTables.Any())
            return;

        await FillCityTable(cancellationToken);
    }

    private async Task FillCityTable(CancellationToken cancellationToken = default)
    {
        var initialCities = new[]
        {
            new CityInfo { Country = "Germany", Name = "Berlin", lat = "52.5170365", lon = "13.3888599"},
            new CityInfo { Country = "Germany", Name = "Frankfurt", lat = "50.1106444", lon = "8.6820917"},
            new CityInfo { Country = "Denmark", Name = "Copenhagen", lat = "55.6867243", lon = "12.5700724"},
            new CityInfo { Country = "United Kingdom", Name = "London", lat = "51.5073219", lon = "-0.1276474"},
            new CityInfo { Country = "United Kingdom", Name = "Manchester", lat = "53.4794892", lon = "-2.2451148"},
            new CityInfo { Country = "Belarus", Name = "Minsk", lat = "53.9024716", lon = "27.5618225"},
            new CityInfo { Country = "Poland", Name = "Warsaw", lat = "52.2319581", lon = "21.0067249"},
            new CityInfo { Country = "Poland", Name = "Poznań", lat = "52.4006632", lon = "16.91973259178088"},
            new CityInfo { Country = "Poland", Name = "Bydgoszcz", lat = "53.1219648", lon = "18.0002529"},
            new CityInfo { Country = "Poland", Name = "Gdańsk", lat = "54.42880315", lon = "18.798325902846855"},
        };
    }
}
