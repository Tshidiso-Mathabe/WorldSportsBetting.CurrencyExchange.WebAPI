using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    public interface ICurrencyRatesService
    {
        Task<LatestResponseDto> GetLatestCurrencyRatesAsync(string? @base = null, CancellationToken cancellationToken = default);
        Task<PaginatedListViewModel<LatestResponseDto>> GetCurrencyRatesHistoryAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default);
    }
}
