using MediatR;

namespace WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Commands
{
    public record DeleteCurrencyRatesHistoryByBaseCommand(string CurrencyRatesBase) :  IRequest;
}
