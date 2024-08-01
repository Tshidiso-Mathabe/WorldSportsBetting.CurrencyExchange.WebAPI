using Microsoft.AspNetCore.Mvc;
using WorldSportsBetting.CurrencyExchange.Core.Services;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRatesController : ControllerBase
    {
        private readonly ICurrencyRatesService _currencyRatesService;

        public CurrencyRatesController(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        /// <summary>
        /// Get latest currency rates from the OpenExchangeRates latest.json API.
        /// </summary>
        /// <param name="base"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Latest")]
        [ProducesResponseType(typeof(LatestResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LatestResponseDto>> GetLatestCurrencyRatesAsync(string? @base = null, CancellationToken cancellationToken = default)
        {
            try
            {
                LatestResponseDto response =
                    string.IsNullOrWhiteSpace(@base) ?
                    await _currencyRatesService.GetLatestCurrencyRatesAsync() :
                    await _currencyRatesService.GetLatestCurrencyRatesAsync(@base?.ToUpper(), cancellationToken);
                return response.HasError ?
                    StatusCode(StatusCodes.Status400BadRequest, response) :
                    StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get database stored currency rates.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("History")]
        [ProducesResponseType(typeof(PaginatedListViewModel<LatestResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedListViewModel<LatestResponseDto>>> GetConversionHistoryAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 100, CancellationToken cancellationToken = default)
        {
            try
            {
                PaginatedListViewModel<LatestResponseDto> currencyRatesHistoryData = await _currencyRatesService.GetCurrencyRatesHistoryAsync(pageIndex, pageSize, cancellationToken);
                return StatusCode(StatusCodes.Status200OK, currencyRatesHistoryData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
