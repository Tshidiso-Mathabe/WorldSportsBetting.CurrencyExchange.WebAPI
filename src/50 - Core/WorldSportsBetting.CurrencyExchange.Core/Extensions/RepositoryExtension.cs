using Microsoft.Extensions.DependencyInjection;
using WorldSportsBetting.CurrencyExchange.Core.Repositories;

namespace WorldSportsBetting.CurrencyExchange.Core.Extensions
{
    public static class RepositoryExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConversionHistoryRepository, ConversionHistoryRepository>();
            services.AddScoped<ICurrencyRatesHistoryRepository, CurrencyRatesHistoryRepository>();
        }
    }
}
