using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.EntityConfigurations
{
    public class CurrencyRatesHistoryEntityConfiguration : IEntityTypeConfiguration<CurrencyRatesHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<CurrencyRatesHistoryEntity> builder)
        {
            builder.ToTable("CurrencyRatesHistory");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(x => x.CreatedOn)
                    .HasDefaultValue(DateTime.Now)
                    .IsRequired();

            builder
                .Property(x => x.Base)
                .IsRequired();

            builder
                .HasIndex(x => x.Base)
                .IsUnique();

            builder
                .Property(x => x.Response)
                .IsRequired();
        }
    }
}
