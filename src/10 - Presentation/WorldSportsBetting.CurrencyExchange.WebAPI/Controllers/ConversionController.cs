using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WorldSportsBetting.CurrencyExchange.Core.Services;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;
using WorldSportsBetting.CurrencyExchange.Domain.ViewModels;

namespace WorldSportsBetting.CurrencyExchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        public readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        /// <summary>
        /// Get currency conversion from the OpenExchangeRates convert API.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ConvertResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConvertResponseDto>> GetConversionAsync([FromQuery][Required] double value, [FromQuery][Required] string from, [FromQuery][Required] string to, CancellationToken cancellationToken)
        {
            try
            {
                ConvertResponseDto response = await _conversionService.GetConversionAsync(value, from, to, cancellationToken);
                return
                    response.HasError ?
                    StatusCode(StatusCodes.Status400BadRequest, response) :
                    StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get database stored currency conversions.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("History")]
        [ProducesResponseType(typeof(PaginatedListViewModel<ConversionHistoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedListViewModel<ConversionHistoryViewModel>>> GetConversionHistoryAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 100, CancellationToken cancellationToken = default)
        {
            try
            {
                PaginatedListViewModel<ConversionHistoryViewModel> conversionHistoryData = await _conversionService.GetConversionHistoryAsync(pageIndex, pageSize, cancellationToken);
                return StatusCode(StatusCodes.Status200OK, conversionHistoryData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
