using MediatR;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Application.ConversionHistories.Queries
{
    public record GetConversionHistoryQuery(int PageIndex = 1, int PageSize = 100) : IRequest<PaginatedListViewModel<ConversionHistoryViewModel>>;
}
