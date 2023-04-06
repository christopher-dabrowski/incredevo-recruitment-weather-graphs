using Azure;
using Azure.Data.Tables;

namespace Shared.Models
{
    public class WeatherInfo : ITableEntity
    {
        public string PartitionKey { get; set; } = Guid.NewGuid().ToString();
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public DateTimeOffset ForecastTime { get; set; } = DateTimeOffset.Now;
        public double TemperatureInCelsius { get; set; }
        public double WindSpeed { get; set; }
    }
}
