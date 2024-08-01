using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.EntityConfigurations
{
    public class ConversionHistoryEntityConfiguration : IEntityTypeConfiguration<ConversionHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<ConversionHistoryEntity> builder)
        {
            builder.ToTable("ConversionHistory");

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
                .Property(x => x.HasError)
                .IsRequired();

            builder
                .Property(x => x.Value);

            builder
                .Property(x => x.From);

            builder
                .Property(x => x.To);

            builder
                .Property(x => x.ConversionValue);

            builder
                .Property(x => x.Response);
        }
    }
}
