namespace WorldSportsBetting.CurrencyExchange.Cache.Options
{
    public class CustomRedisCacheOptions
    {
        public const string SECTION = "RedisCacheConnectionString";

        public required int CacheTimeSpanInMinutes { get; set; }
    }
}
