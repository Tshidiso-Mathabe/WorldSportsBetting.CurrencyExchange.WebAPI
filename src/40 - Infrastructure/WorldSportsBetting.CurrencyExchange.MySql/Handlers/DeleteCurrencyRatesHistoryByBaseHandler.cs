using MediatR;
using Microsoft.EntityFrameworkCore;
using WorldSportsBetting.CurrencyExchange.Application.CurrencyRatesHistories.Commands;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.Handlers
{
    internal class DeleteCurrencyRatesHistoryByBaseHandler : IRequestHandler<DeleteCurrencyRatesHistoryByBaseCommand>
    {
        private readonly WSBCurrencyExchangeDbContext _dbContext;

        public DeleteCurrencyRatesHistoryByBaseHandler(WSBCurrencyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteCurrencyRatesHistoryByBaseCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _dbContext.DbContext.Database.BeginTransaction();
            try
            {
                CurrencyRatesHistoryEntity? currencyRatesHistoryEntity = await _dbContext.CurrencyRatesHistory.FirstOrDefaultAsync(x => x.Base == request.CurrencyRatesBase, cancellationToken);

                if (currencyRatesHistoryEntity == null) { return; }

                _dbContext.CurrencyRatesHistory.Remove(currencyRatesHistoryEntity);
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
