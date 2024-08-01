using MediatR;
using WorldSportsBetting.CurrencyExchange.Application.ConversionHistories.Commands;
using WorldSportsBetting.CurrencyExchange.Application.ConversionHistories.Queries;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Repositories
{
    internal class ConversionHistoryRepository : IConversionHistoryRepository
    {
        private IMediator _mediator;

        public ConversionHistoryRepository(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task AddAsync(ConvertResponseDto convertResponseDto, CancellationToken cancellationToken)
        {
            return _mediator.Send(new CreateConversionHistoryCommand(convertResponseDto), cancellationToken);
        }

        public Task<PaginatedListViewModel<ConversionHistoryViewModel>> GetAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(new GetConversionHistoryQuery(pageIndex, pageSize), cancellationToken);
        }
    }
}
