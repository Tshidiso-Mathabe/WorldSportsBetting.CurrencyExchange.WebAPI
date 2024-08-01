using MediatR;
using Microsoft.Extensions.Configuration;
using WorldSportsBetting.CurrencyExchange.Application.OpenExchangeRates.Queries;
using WorldSportsBetting.CurrencyExchange.Cache;
using WorldSportsBetting.CurrencyExchange.Core.Repositories;
using WorldSportsBetting.CurrencyExchange.Domain.Constants;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    internal class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly IMediator _mediator;
        private readonly ICurrencyRatesHistoryRepository _repository;
        private readonly CustomRedisCache _cache;
        private readonly IConfiguration _configuration;

        public CurrencyRatesService(IMediator mediator, ICurrencyRatesHistoryRepository repository, CustomRedisCache cache, IConfiguration configuration)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<LatestResponseDto> GetLatestCurrencyRatesAsync(string? @base = null, CancellationToken cancellationToken = default)
        {
            string? currencyBase = string.IsNullOrWhiteSpace(@base) ? _configuration["DefaultBaseCurrency"] : @base;
            string cacheKey = $"{RedisCacheConstants.CACHE_KEY}_{currencyBase}";
            LatestResponseDto? latestResponseDto = await _cache.GetAsync<LatestResponseDto>(cacheKey, cancellationToken);
            if (latestResponseDto == null)
            {
                latestResponseDto = await _mediator.Send(new GetLatestCurrencyRatesQuery(@base), cancellationToken);
                if (latestResponseDto.HasError) { return latestResponseDto; }
                await _cache.SetAsync(cacheKey, latestResponseDto, default, default, cancellationToken);
                await _repository.DeleteAsync(latestResponseDto.Base, cancellationToken);
                await _repository.AddAsync(latestResponseDto, cancellationToken);
            }
            return latestResponseDto;
        }

        public Task<PaginatedListViewModel<LatestResponseDto>> GetCurrencyRatesHistoryAsync(int pageIndex = 1, int pageSize = 100, CancellationToken cancellationToken = default)
        {
            return _repository.GetAsync(pageIndex, pageSize, cancellationToken);
        }
    }
}
