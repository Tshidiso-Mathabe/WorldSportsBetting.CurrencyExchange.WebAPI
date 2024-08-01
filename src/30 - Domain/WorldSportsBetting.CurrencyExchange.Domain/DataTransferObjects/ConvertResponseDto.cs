using Newtonsoft.Json;

namespace WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects
{
    public class ConvertResponseDto
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("request")]
        public ConvertRequestResponseDto Request { get; set; }

        [JsonProperty("meta")]
        public ConvertMetaResponseDto Meta { get; set; }

        [JsonProperty("response")]
        public double Response { get; set; }

        public bool HasError { get; set; } = false;

        public ConvertErrorResponseDto? ErrorDetails { get; set; } = null;
    }
}
