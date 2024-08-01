using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    internal class ConvertService : IConvertService
    {
        private readonly ICurrencyRatesService _currencyRatesService;

        public ConvertService(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        public async Task<CalcConvertResponseDto> ConvertAsync(string @base, string target, double amount, CancellationToken cancellationToken)
        {
            LatestResponseDto latestResponseDto = await _currencyRatesService.GetLatestCurrencyRatesAsync(@base, cancellationToken);
            if (latestResponseDto.HasError) { return new CalcConvertResponseDto { Success = false, ErrorMessage = JsonConvert.SerializeObject(latestResponseDto.ErrorDetails) }; }
            Dictionary<string, double> currencyRates = latestResponseDto.Rates;
            if (!currencyRates.ContainsKey(target)) { return new CalcConvertResponseDto { Success = false, ErrorMessage = $"'{target}' currency is not available." }; }
            double currencyRate = currencyRates[target];
            double result = amount * currencyRate;
            return new CalcConvertResponseDto { Success = true, Result = result };
        }
    }
}
