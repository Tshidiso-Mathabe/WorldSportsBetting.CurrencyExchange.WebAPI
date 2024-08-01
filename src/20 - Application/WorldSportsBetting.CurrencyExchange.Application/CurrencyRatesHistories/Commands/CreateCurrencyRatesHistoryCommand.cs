using MediatR;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Commands
{
    public record CreateCurrencyRatesHistoryCommand(LatestResponseDto LatestResponseDto) : IRequest;
}
