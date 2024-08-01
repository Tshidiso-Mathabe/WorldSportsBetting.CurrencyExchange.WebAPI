using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WorldSportsBetting.CurrencyExchange.Core.Services;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly IConvertService _convertService;

        public ConvertController(IConvertService convertService)
        {
             _convertService = convertService;
        }

        /// <summary>
        /// Convert currency.
        /// </summary>
        /// <param name="base"></param>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CalcConvertResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalcConvertResponseDto>> ConvertAsync([FromQuery][Required] string @base, [FromQuery][Required] string target, [FromQuery][Required] double amount, CancellationToken cancellationToken)
        {
            try
            {
                CalcConvertResponseDto response = await _convertService.ConvertAsync(@base.ToUpper(), target.ToUpper(), amount, cancellationToken);
                return
                    response.Success ?
                    StatusCode(StatusCodes.Status200OK, response) :
                    StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
