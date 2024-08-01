using MediatR;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Queries
{
    public record GetCurrencyRatesHistoryQuery(int PageIndex = 1, int PageSize = 100) : IRequest<PaginatedListViewModel<LatestResponseDto>>;
}
