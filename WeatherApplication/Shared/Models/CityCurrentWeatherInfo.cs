using Azure;
using Azure.Data.Tables;

namespace Shared.Models;

public class CityCurrentWeatherInfo : ITableEntity
{
    public string PartitionKey { get; set; } = Guid.NewGuid().ToString();
    public string RowKey { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string Country { get; set; }
    public string Name { get; set; }

    public string lat { get; set; }
    public string lon { get; set; }

    public double TemperatureInCelsius { get; set; }
    public double WindSpeed { get; set; }
}
