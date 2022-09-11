using Br.Capegemini.Teste._09._2022.Models;
using Br.Capegemini.Teste._09._2022.Services;
using Microsoft.AspNetCore.Mvc;

namespace Br.Capegemini.Teste._09._2022.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly TokenService _service;
        public TokenController(TokenService service)
        {
            _service = service;
        }

        [HttpPost("Generate")]
        public async Task<IActionResult> GenerateToken([FromBody]UserRegistration user)
        {
            try
            {
                return Ok(_service.GenerateToken(user));
                
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateToken([FromBody]ValidateToken token)
        {
            try
            {
                return Ok(await _service.ValidateToken(token));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
