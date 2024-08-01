using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Queries;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.Handlers
{
    internal class GetCurrencyRatesHistoryHandler : IRequestHandler<GetCurrencyRatesHistoryQuery, PaginatedListViewModel<LatestResponseDto>>
    {
        private readonly WSBCurrencyExchangeDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCurrencyRatesHistoryHandler(WSBCurrencyExchangeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedListViewModel<LatestResponseDto>> Handle(GetCurrencyRatesHistoryQuery request, CancellationToken cancellationToken)
        {
            CurrencyRatesHistoryViewModel[] currencyRatesHistoryArray = await Task.FromResult(_mapper.Map<CurrencyRatesHistoryEntity[], CurrencyRatesHistoryViewModel[]>(_dbContext.CurrencyRatesHistory.OrderBy(x => x.Id).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToArray()));
            List<LatestResponseDto> latestResponseDtos = new List<LatestResponseDto>();
            foreach (CurrencyRatesHistoryViewModel currencyRatesHistory in currencyRatesHistoryArray)
            {
                string currencyRatesHistoryResponse = currencyRatesHistory.Response;
                LatestResponseDto? latestResponseDto = JsonConvert.DeserializeObject<LatestResponseDto>(currencyRatesHistoryResponse);
                latestResponseDtos.Add(latestResponseDto);
            }
            var count = _dbContext.CurrencyRatesHistory.Count();
            var totalPages = (int)Math.Ceiling(count / (double)request.PageSize);

            return new PaginatedListViewModel<LatestResponseDto>(latestResponseDtos.ToArray(), request.PageIndex, totalPages);
        }
    }
}
