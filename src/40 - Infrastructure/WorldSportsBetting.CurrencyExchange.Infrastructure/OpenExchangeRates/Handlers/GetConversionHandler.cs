using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Application.OpenExchangeRates.Queries;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Options;

namespace WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Handlers
{
    public class GetConversionHandler : IRequestHandler<GetConversionQuery, ConvertResponseDto>
    {
        private readonly OpenExchangeRatesApiOptions _options;

        public GetConversionHandler(IOptions<OpenExchangeRatesApiOptions> options)
        {
            _options = options.Value;
        }

        public async Task<ConvertResponseDto> Handle(GetConversionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(_options.BaseUrl) };
                using HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"{_options.ConvertEndpoint}{request.Value}/{request.From}/{request.To}?app_id={_options.AppId}", cancellationToken);
                string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                if (jsonResult.Contains("error"))
                {
                    ConvertErrorResponseDto? errorObject = JsonConvert.DeserializeObject<ConvertErrorResponseDto>(jsonResult);
                    return new ConvertResponseDto { HasError = true, ErrorDetails = errorObject };
                }
                ConvertResponseDto? successObject = JsonConvert.DeserializeObject<ConvertResponseDto>(jsonResult);
                return successObject;
            }
            catch (Exception ex)
            {
                return new ConvertResponseDto { HasError = true, ErrorDetails = new ConvertErrorResponseDto { Error = true, Status = 500, Message = "Exception", Description = ex.Message } };
            }
        }
    }
}
