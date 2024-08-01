namespace WorldSportsBetting.CurrencyExchange.Domain.ViewModels
{
    public class ConversionHistoryViewModel
    {
        public int Id { get; set; }

        public DateTime CreateOn { get; set; }

        public bool HasError { get; set; }

        public double? Value { get; set; }

        public string? From { get; set; }

        public string? To { get; set; }

        public double? ConversionValue { get; set; }

        public string Response { get; set; }
    }
}
