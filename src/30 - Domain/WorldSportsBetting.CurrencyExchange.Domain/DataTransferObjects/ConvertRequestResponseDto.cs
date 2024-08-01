using Newtonsoft.Json;

namespace WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects
{
    public class ConvertRequestResponseDto
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}
