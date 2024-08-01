using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldSportsBetting.CurrencyExchange.MySql.AutoMapperProfiles;
using WorldSportsBetting.CurrencyExchange.MySql.Options;

namespace WorldSportsBetting.CurrencyExchange.MySql.Extensions
{
    public static class MySqlExtension
    {
        public static void RegisterMySqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlDatabaseOptions>(configuration.GetSection(MySqlDatabaseOptions.SECTION));
            services.AddDbContext<WSBCurrencyExchangeDbContext>();
        }

        public static void RegisterMySqlAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(ca => ca.AddProfile<WSBCurrencyExchangeMySqlMappingProfile>());
        }
    }
}
