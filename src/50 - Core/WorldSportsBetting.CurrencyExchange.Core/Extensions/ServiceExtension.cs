using Microsoft.Extensions.DependencyInjection;
using WorldSportsBetting.CurrencyExchange.Core.Services;

namespace WorldSportsBetting.CurrencyExchange.Core.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IConversionService, ConversionService>();
            services.AddScoped<ICurrencyRatesService, CurrencyRatesService>();
            services.AddScoped<IConvertService, ConvertService>();
        }
    }
}
