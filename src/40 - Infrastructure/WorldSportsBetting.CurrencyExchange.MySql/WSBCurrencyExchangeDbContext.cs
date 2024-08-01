using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;
using WorldSportsBetting.CurrencyExchange.MySql.Options;

namespace WorldSportsBetting.CurrencyExchange.MySql
{
    internal class WSBCurrencyExchangeDbContext : DbContext
    {
        private readonly MySqlDatabaseOptions _options;

        public WSBCurrencyExchangeDbContext(
            DbContextOptions<WSBCurrencyExchangeDbContext> dbContextOptions,
            IOptions<MySqlDatabaseOptions> options)
            : base(dbContextOptions)
        {
            _options = options.Value;
            DbContext = this;
        }

        public DbContext DbContext { get; }

        public DbSet<ConversionHistoryEntity> ConversionHistory { get; set; }
        public DbSet<CurrencyRatesHistoryEntity> CurrencyRatesHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (_options.UseInMemory)
            {
                optionsBuilder.UseInMemoryDatabase("WSBCurrencyExchangeInMemDb");
                optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)); // Transaction not supported in-memory database therefore ignore warning.
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
            }
            else
            {
                optionsBuilder.UseMySql(_options.WSBCurrencyExchange, ServerVersion.AutoDetect(_options.WSBCurrencyExchange));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
