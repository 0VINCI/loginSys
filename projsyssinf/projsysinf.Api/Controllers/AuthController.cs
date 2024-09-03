using Microsoft.AspNetCore.Mvc;
using projsysinf.Application.Commands;
using projsysinf.Application.Dto;
using profsysinf.Core.Exceptions;

namespace projsysinf.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController(ICommandDispatcher commandDispatcher) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInDto dto)
        {
            try
            {
                var command = new SignInCommand(dto.Email, dto.Password);
                var response = await commandDispatcher.SendAsync<SignInCommand, SignInResponseDto>(command);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.Now.AddMinutes(60),
                    SameSite = SameSiteMode.None,

                };

                Response.Cookies.Append("AuthToken", response.Token, cookieOptions);

                return Ok(new 
                { 
                    userId = response.UserId, 
                    history = response.History 
                });
            }
            catch (UserNotFoundException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { error = ex.Message });
            }
            catch (InvalidPasswordException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new JsonResult(new { error = ex.Message });
            }
            catch (UserIsLockedException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return new JsonResult(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var command = new RegisterCommand(dto.Email, dto.Password, dto.ConfirmPassword);
                var result = await commandDispatcher.SendAsync<RegisterCommand, string>(command);

                return Created("", result);
            }
            catch (UserAlreadyExistsException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return new JsonResult(new { error = ex.Message });
            }
            catch (PasswordNotTheSameException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                var command = new ChangePasswordCommand(dto.Email, dto.OldPassword, dto.NewPassword);
                var result = await commandDispatcher.SendAsync<ChangePasswordCommand, string>(command);

                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { error = ex.Message });
            }
            catch (PasswordHasBeenUsedException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(new { error = ex.Message });
            }
            catch (InvalidPasswordException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new JsonResult(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpPost("passwordReminder")]
        public async Task<IActionResult> PasswordReminder([FromBody] PasswordReminderDto dto)
        {
            try
            {
                var command = new PasswordReminderCommand(dto.Email);
                var result = await commandDispatcher.SendAsync<PasswordReminderCommand, string>(command);

                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
        {
            try
            {
                Response.Cookies.Delete("AuthToken");

                var command = new LogoutCommand(dto.UserId);
                await commandDispatcher.SendAsync(command);

                return Ok(new { message = "User logged out successfully." });
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }
    }
}
