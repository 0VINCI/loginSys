using Microsoft.AspNetCore.Mvc;
using projsysinf.Application.Commands;
using projsysinf.Application.Dto;

namespace projsysinf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ICommandDispatcher commandDispatcher) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInDto dto)
        {
            var command = new SignInCommand(dto.Email, dto.Password);
            var token = await commandDispatcher.SendAsync<SignInCommand, string>(command);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddMinutes(60)
            };

            Response.Cookies.Append("AuthToken", token, cookieOptions);

            return Ok();
        }
    }
}