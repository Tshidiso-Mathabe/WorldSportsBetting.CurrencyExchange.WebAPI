using Newtonsoft.Json;

namespace WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects
{
    public class LatestResponseDto
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }

        public bool HasError { get; set; } = false;

        public LatestErrorResponseDto? ErrorDetails { get; set; } = null;
    }
}
