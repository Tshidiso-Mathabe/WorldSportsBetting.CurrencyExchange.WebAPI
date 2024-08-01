using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Core.Services
{
    public interface IConvertService
    {
        Task<CalcConvertResponseDto> ConvertAsync(string @base, string target, double amount, CancellationToken cancellationToken);
    }
}
