using AutoMapper;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.AutoMapperProfiles.Extensions
{
    internal static class WSBCurrencyExchangeMySqlCurrencyRatesHistoryMappingProfile
    {
        public static void ConfigureCurrencyRatesHistoryMappingProfile(this Profile profile)
        {
            profile.CreateMap<CurrencyRatesHistoryEntity, CurrencyRatesHistoryViewModel>().ReverseMap();
        }
    }
}
