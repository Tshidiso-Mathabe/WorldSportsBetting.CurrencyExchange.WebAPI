using Newtonsoft.Json;

namespace WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects
{
    public class ConvertErrorResponseDto
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
