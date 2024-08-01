using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Options;

namespace WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Extensions
{
    public static class OpenExchangeRatesApiOptionsExtention
    {
        public static void RegisterOpenExchangeRatesApiOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OpenExchangeRatesApiOptions>(configuration.GetSection(OpenExchangeRatesApiOptions.SECTION));
        }
    }
}
