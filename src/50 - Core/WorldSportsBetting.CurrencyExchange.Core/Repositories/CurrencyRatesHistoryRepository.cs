using MediatR;
using WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Commands;
using WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Queries;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Repositories
{
    internal class CurrencyRatesHistoryRepository : ICurrencyRatesHistoryRepository
    {
        private IMediator _mediator;

        public CurrencyRatesHistoryRepository(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task AddAsync(LatestResponseDto latestResponseDto, CancellationToken cancellationToken)
        {
            return _mediator.Send(new CreateCurrencyRatesHistoryCommand(latestResponseDto), cancellationToken);
        }

        public Task<PaginatedListViewModel<LatestResponseDto>> GetAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(new GetCurrencyRatesHistoryQuery(pageIndex, pageSize), cancellationToken);
        }

        public Task DeleteAsync(string currencyRatesBase, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteCurrencyRatesHistoryByBaseCommand(currencyRatesBase), cancellationToken);
        }
    }
}
