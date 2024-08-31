using Microsoft.AspNetCore.Mvc;
using projsysinf.Application.Commands;
using projsysinf.Application.Dto;

namespace projsysinf.Api.Controllers
{
    [Route("[controller]")]
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var command = new RegisterCommand(dto.Email, dto.Password, dto.ConfirmPassword);
            var result = await commandDispatcher.SendAsync<RegisterCommand, string>(command);
            
            return Ok(result);
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var command = new ChangePasswordCommand(dto.Email, dto.OldPassword, dto.NewPassword);
            var result = await commandDispatcher.SendAsync<ChangePasswordCommand, string>(command);
            
            return Ok(result);
        }
        [HttpPost("passwordReminder")]
        public async Task<IActionResult> PasswordReminder([FromBody] PasswordReminderDto dto)
        {
            var command = new PasswordReminderCommand(dto.Email);
            var result = await commandDispatcher.SendAsync<PasswordReminderCommand, string>(command);
            
            return Ok(result);
        }
    }
}