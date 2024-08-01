namespace WorldSportsBetting.CurrencyExchange.Domain.ViewModels
{
    public class CurrencyRatesHistoryViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Base { get; set; }

        public string Response { get; set; }
    }
}
