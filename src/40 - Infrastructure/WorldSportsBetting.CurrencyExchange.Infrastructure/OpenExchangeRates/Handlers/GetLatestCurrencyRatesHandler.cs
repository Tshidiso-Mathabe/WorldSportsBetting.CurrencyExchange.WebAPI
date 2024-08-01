using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WorldSportsBetting.CurrencyExchange.Application.OpenExchangeRates.Queries;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Options;

namespace WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Handlers
{
    public class GetLatestCurrencyRatesHandler : IRequestHandler<GetLatestCurrencyRatesQuery, LatestResponseDto>
    {
        private readonly OpenExchangeRatesApiOptions _options;

        public GetLatestCurrencyRatesHandler(IOptions<OpenExchangeRatesApiOptions> options)
        {
            _options = options.Value;
        }

        public async Task<LatestResponseDto> Handle(GetLatestCurrencyRatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(_options.BaseUrl) };
                string requestUri = string.IsNullOrWhiteSpace(request.Base) ? $"{_options.LatestEndpoint}?app_id={_options.AppId}" : $"{_options.LatestEndpoint}?app_id={_options.AppId}&base={request.Base}";
                using HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUri, cancellationToken);
                string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                if (jsonResult.Contains("error"))
                {
                    return new LatestResponseDto { HasError = true, ErrorDetails = new LatestErrorResponseDto { Message = "Please ensure the correct base is being used.", Result = jsonResult } };
                }
                LatestResponseDto? successObject = JsonConvert.DeserializeObject<LatestResponseDto>(jsonResult);
                return successObject;
            }
            catch (Exception ex)
            {
                return new LatestResponseDto { HasError = true, ErrorDetails = new LatestErrorResponseDto { Message = "Exception", Result = ex.Message } };
            }
        }
    }
}
