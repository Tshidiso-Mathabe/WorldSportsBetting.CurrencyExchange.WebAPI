using AutoMapper;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;
using WorldSportsBetting.CurrencyExchange.MySql.Entities;

namespace WorldSportsBetting.CurrencyExchange.MySql.AutoMapperProfiles.Extensions
{
    internal static class WSBCurrencyExchangeMySqlConversionHistoryMappingProfile
    {
        public static void ConfigureConversionHistoryMappingProfile(this Profile profile)
        {
            profile.CreateMap<ConversionHistoryEntity, ConversionHistoryViewModel>().ReverseMap();
        }
    }
}
