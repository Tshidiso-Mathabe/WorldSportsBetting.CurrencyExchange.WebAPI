using MediatR;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Application.OpenExchangeRates.Queries
{
    public record GetLatestCurrencyRatesQuery(string? Base = null) : IRequest<LatestResponseDto>;
}
