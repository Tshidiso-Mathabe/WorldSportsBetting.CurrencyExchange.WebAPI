using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Core.Repositories;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    internal class ConvertService : IConvertService
    {
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly IConversionHistoryRepository _conversionHistoryRepository;

        public ConvertService(ICurrencyRatesService currencyRatesService, IConversionHistoryRepository conversionHistoryRepository)
        {
            _currencyRatesService = currencyRatesService;
            _conversionHistoryRepository = conversionHistoryRepository;
        }

        public async Task<CalcConvertResponseDto> ConvertAsync(string @base, string target, double amount, CancellationToken cancellationToken)
        {
            LatestResponseDto latestResponseDto = await _currencyRatesService.GetLatestCurrencyRatesAsync(@base, cancellationToken);
            if (latestResponseDto.HasError) { return new CalcConvertResponseDto { Success = false, ErrorMessage = JsonConvert.SerializeObject(latestResponseDto.ErrorDetails) }; }
            Dictionary<string, double> currencyRates = latestResponseDto.Rates;
            if (!currencyRates.ContainsKey(target)) { return new CalcConvertResponseDto { Success = false, ErrorMessage = $"'{target}' currency is not available." }; }
            double currencyRate = currencyRates[target];
            double result = amount * currencyRate;
            await _conversionHistoryRepository.AddAsync(new ConvertResponseDto { HasError = false, Request = new ConvertRequestResponseDto { Amount = amount, From = @base, To = target }, Meta = new ConvertMetaResponseDto { Rate = result } }, cancellationToken);
            return new CalcConvertResponseDto { Success = true, Result = result };
        }
    }
}
