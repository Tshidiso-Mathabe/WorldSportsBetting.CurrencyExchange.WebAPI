using Microsoft.EntityFrameworkCore;
using WorldSportsBetting.CurrencyExchange.MySql.EntityConfigurations;

namespace WorldSportsBetting.CurrencyExchange.MySql.Entities
{
    [EntityTypeConfiguration(typeof(CurrencyRatesHistoryEntityConfiguration))]
    public sealed class CurrencyRatesHistoryEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Base { get; set; }

        public string Response { get; set; }
    }
}
