using MediatR;
using WorldSportsBetting.CurrencyExchange.Application.OpenExchangeRates.Queries;
using WorldSportsBetting.CurrencyExchange.Core.Repositories;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    internal class ConversionService : IConversionService
    {
        private readonly IMediator _mediator;
        private readonly IConversionHistoryRepository _repository;

        public ConversionService(IMediator mediator, IConversionHistoryRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<ConvertResponseDto> GetConversionAsync(double value, string from, string to, CancellationToken cancellationToken)
        {
            ConvertResponseDto convertResponseDto = await _mediator.Send(new GetConversionQuery(value, from, to), cancellationToken);
            await _repository.AddAsync(convertResponseDto, cancellationToken);
            return convertResponseDto;
        }

        public Task<PaginatedListViewModel<ConversionHistoryViewModel>> GetConversionHistoryAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default)
        {
            return _repository.GetAsync(pageIndex, pageSize, cancellationToken);
        }
    }
}
