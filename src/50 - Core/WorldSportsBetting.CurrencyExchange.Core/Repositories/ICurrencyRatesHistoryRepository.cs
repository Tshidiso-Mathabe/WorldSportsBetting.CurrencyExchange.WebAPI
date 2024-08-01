using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Repositories
{
    internal interface ICurrencyRatesHistoryRepository
    {
        Task AddAsync(LatestResponseDto latestResponseDto, CancellationToken cancellationToken);

        Task<PaginatedListViewModel<LatestResponseDto>> GetAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default);

        Task DeleteAsync(string currencyRatesBase, CancellationToken cancellationToken);
    }
}
