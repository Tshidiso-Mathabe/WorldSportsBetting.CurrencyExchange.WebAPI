using AutoMapper;
using WorldSportsBetting.CurrencyExchange.MySql.AutoMapperProfiles.Extensions;

namespace WorldSportsBetting.CurrencyExchange.MySql.AutoMapperProfiles
{
    internal class WSBCurrencyExchangeMySqlMappingProfile : Profile
    {
        public WSBCurrencyExchangeMySqlMappingProfile()
        {
            this.ConfigureConversionHistoryMappingProfile();
            this.ConfigureCurrencyRatesHistoryMappingProfile();
        }
    }
}
