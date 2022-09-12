using Br.Capegemini.Teste._09._2022.Models;
using Br.Capegemini.Teste._09._2022.Services;
using Br.Capegemini.Teste._09._2022.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Br.Capegemini.Teste._09._2022.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    public class TokenController : Controller
    {
        private readonly ITokenService _service;
        public TokenController(ITokenService service)
        {
            _service = service;
        }


        /// <summary>
        /// Create Token with credit card and costumer data for charge and validation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/Token/Generate
        ///     
        ///     CVV must be between 100 and 99999
        ///     Card Number must contain 16 characters  starting ate least with 1
        ///     Amount must have maximum 2 decimal digits, if x,00 do not use.
        ///
        /// </remarks>
        /// <returns>IEnumerable of slugs</returns>
        /// <response code="200">If all requested items are found</response>
        /// <response code="400">If request have some error or body is incorrect</response>
        /// <response code="404">If request not found</response>
        /// <response code="404">If request not found</response>
        [HttpPost("Generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateToken([FromBody]UserRegistration user)
        {
            try
            {
                return Ok(_service.GenerateToken(user));
                
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine($"Value not in Range: {ex}");
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                throw ex;
            }
        }


        /// <summary>
        /// Validate Cashless Token with costumer and Token creation agains Credit card data
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/Token/validate
        ///     
        ///     CVV must be between 100 and 99999
        ///     Token that was generated in previous Http request
        ///
        /// </remarks>
        /// <returns>IEnumerable of slugs</returns>
        /// <response code="200">If all requested items are found</response>
        /// <response code="400">If request have some error or body is incorrect</response>
        /// <response code="404">If request not found</response>
        /// <response code="500">Internal Server error, contact team to more information</response>
        [HttpPost("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ValidateToken([FromBody]ValidateToken token)
        {
            try
            {
                return Ok(await _service.ValidateToken(token));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                throw ex;
            }
        }


    }
}
