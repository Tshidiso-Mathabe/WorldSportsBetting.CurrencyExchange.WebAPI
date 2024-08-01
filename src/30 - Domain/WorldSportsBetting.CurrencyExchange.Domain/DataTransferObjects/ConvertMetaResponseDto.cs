using Newtonsoft.Json;

namespace WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects
{
    public  class ConvertMetaResponseDto
    {
        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
    }
}
