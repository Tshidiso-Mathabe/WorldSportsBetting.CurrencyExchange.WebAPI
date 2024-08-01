namespace WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects
{
    public class CalcConvertResponseDto
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public double? Result { get; set; }
    }
}
