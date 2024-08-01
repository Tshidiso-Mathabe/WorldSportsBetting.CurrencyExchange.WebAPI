using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Cache.Options;

namespace WorldSportsBetting.CurrencyExchange.Cache
{
    public class CustomRedisCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly CustomRedisCacheOptions _options;

        public CustomRedisCache(IDistributedCache distributedCache, IOptions<CustomRedisCacheOptions> options)
        {
            _distributedCache = distributedCache;
            _options = options.Value;
        }

        public async Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null, CancellationToken cancellationToken = default)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(_options.CacheTimeSpanInMinutes); // How long data should be cached before it is removed from cache - default is set to cache data for only 15 minutes.
            options.SlidingExpiration = unusedExpireTime; // How long data should be cached if not access.

            string jsonData = JsonConvert.SerializeObject(data);

            await _distributedCache.SetStringAsync(key, jsonData, options, cancellationToken);
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            string? jsonData = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (jsonData is null)
            {
                return default(T?);
            }

            return JsonConvert.DeserializeObject<T?>(jsonData);
        }
    }
}
