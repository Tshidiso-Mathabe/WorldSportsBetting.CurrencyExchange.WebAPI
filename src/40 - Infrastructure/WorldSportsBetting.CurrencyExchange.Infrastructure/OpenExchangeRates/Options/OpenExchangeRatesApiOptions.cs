namespace WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Options
{
    public class OpenExchangeRatesApiOptions
    {
        public const string SECTION = "OpenExchangeRatesAPI";

        public required string BaseUrl { get; set; }
        public required string AppId { get; set; }
        public required string LatestEndpoint { get; set; }
        public required string ConvertEndpoint { get; set; }
    }
}
