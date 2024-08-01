using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    public interface IConversionService
    {
        Task<ConvertResponseDto> GetConversionAsync(double value, string from, string to, CancellationToken cancellationToken);
        Task<PaginatedListViewModel<ConversionHistoryViewModel>> GetConversionHistoryAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default);
    }
}
