using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Repositories
{
    internal interface IConversionHistoryRepository
    {
        Task AddAsync(ConvertResponseDto convertResponseDto, CancellationToken cancellationToken);
        Task<PaginatedListViewModel<ConversionHistoryViewModel>> GetAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default);
    }
}
