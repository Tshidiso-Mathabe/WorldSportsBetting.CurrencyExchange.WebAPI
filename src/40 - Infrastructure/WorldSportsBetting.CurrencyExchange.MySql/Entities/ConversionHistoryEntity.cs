using Microsoft.EntityFrameworkCore;
using WorldSportsBetting.CurrencyExchange.MySql.EntityConfigurations;

namespace WorldSportsBetting.CurrencyExchange.MySql.Entities
{
    [EntityTypeConfiguration(typeof(ConversionHistoryEntityConfiguration))]
    public sealed class ConversionHistoryEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool HasError { get; set; }

        public double? Value { get; set; }

        public string? From { get; set; }

        public string? To { get; set; }

        public double? ConversionValue { get; set; }

        public string Response { get; set; }
    }
}
