using MediatR;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Application.OpenExchangeRates.Queries
{
    public record GetConversionQuery(double Value, string From, string To) : IRequest<ConvertResponseDto>;
}
