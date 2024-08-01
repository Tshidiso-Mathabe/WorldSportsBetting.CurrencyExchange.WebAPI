using AutoMapper;
using MediatR;
using WorldSportsBetting.CurrencyExchange.Application.ConversionHistories.Queries;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.Handlers
{
    internal class GetConversionHistoryHandler : IRequestHandler<GetConversionHistoryQuery, PaginatedListViewModel<ConversionHistoryViewModel>>
    {
        private readonly WSBCurrencyExchangeDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetConversionHistoryHandler(WSBCurrencyExchangeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedListViewModel<ConversionHistoryViewModel>> Handle(GetConversionHistoryQuery request, CancellationToken cancellationToken)
        {
            ConversionHistoryViewModel[] conversionHistoryArray = await Task.FromResult(_mapper.Map<ConversionHistoryEntity[], ConversionHistoryViewModel[]>(_dbContext.ConversionHistory.OrderBy(x => x.Id).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToArray()));

            var count = _dbContext.ConversionHistory.Count();
            var totalPages = (int)Math.Ceiling(count / (double)request.PageSize);

            return new PaginatedListViewModel<ConversionHistoryViewModel>(conversionHistoryArray, request.PageIndex, totalPages);
        }
    }
}
