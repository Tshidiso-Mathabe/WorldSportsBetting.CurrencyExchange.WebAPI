using MediatR;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Application.ConversionHistories.Commands
{
    public record CreateConversionHistoryCommand(ConvertResponseDto ConvertResponseDto) : IRequest;
}
