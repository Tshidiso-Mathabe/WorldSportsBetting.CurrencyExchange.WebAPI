using MediatR;
using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Commands;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.Handlers
{
    internal class CreateCurrencyRatesHistoryHandler : IRequestHandler<CreateCurrencyRatesHistoryCommand>
    {
        private readonly WSBCurrencyExchangeDbContext _dbContext;

        public CreateCurrencyRatesHistoryHandler(WSBCurrencyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateCurrencyRatesHistoryCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _dbContext.DbContext.Database.BeginTransaction();
            try
            {
                var latestResponseDto = request.LatestResponseDto;
                var currencyRatesHistoryEntity =
                    new CurrencyRatesHistoryEntity
                    {
                        Base = latestResponseDto.Base,
                        Response = JsonConvert.SerializeObject(latestResponseDto)
                    };

                await _dbContext.CurrencyRatesHistory.AddAsync(currencyRatesHistoryEntity, cancellationToken);
                await _dbContext.DbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
