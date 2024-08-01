using MediatR;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;
using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Application.ConversionHistories.Commands;

namespace WorldSportsBetting.CurrencyExchange.MySql.Handlers
{
    internal class CreateConversionHistoryHandler : IRequestHandler<CreateConversionHistoryCommand>
    {
        private readonly WSBCurrencyExchangeDbContext _dbContext;

        public CreateConversionHistoryHandler(WSBCurrencyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateConversionHistoryCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _dbContext.DbContext.Database.BeginTransaction();
            try
            {
                var convertResponseDto = request.ConvertResponseDto;
                var conversionHistoryEntity =
                    new ConversionHistoryEntity
                    {
                        HasError = convertResponseDto.HasError,
                        Value = convertResponseDto?.Request?.Amount,
                        From = convertResponseDto?.Request?.From,
                        To = convertResponseDto?.Request?.To,
                        ConversionValue = convertResponseDto?.Meta?.Rate,
                        Response = JsonConvert.SerializeObject(convertResponseDto)
                    };

                await _dbContext.ConversionHistory.AddAsync(conversionHistoryEntity, cancellationToken);
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
