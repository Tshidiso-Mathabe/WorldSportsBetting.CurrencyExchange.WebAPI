using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldSportsBetting.CurrencyExchange.Cache.Options;
using WorldSportsBetting.CurrencyExchange.Domain.Constants;

namespace WorldSportsBetting.CurrencyExchange.Cache.Extensions
{
    public static class RedisExtension
    {
        public static void RegisterRedis(this IServiceCollection services, IConfiguration configuration)
        {
            string appSettingsSection = "RedisCacheConnectionString";
            IConfigurationSection configurationSection = configuration.GetSection(appSettingsSection);
            services.Configure<CustomRedisCacheOptions>(configurationSection);
            string ? redisServer = configurationSection["Server"];
            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = redisServer;
                o.InstanceName = RedisCacheConstants.KEY_PREFIX;
            });
        }

        public static void RegisterRedisCacheInstance(this IServiceCollection services)
        {
            services.AddSingleton<CustomRedisCache>();
        }
    }
}
