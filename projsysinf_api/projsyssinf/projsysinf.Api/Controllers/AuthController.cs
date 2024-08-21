using Microsoft.AspNetCore.Mvc;
using projsysinf.Application.Commands;
using projsysinf.Application.Dto;

namespace projsysinf.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AuthController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SingInDto dto)
    {
        var command = new SignInCommand(dto.Email, dto.Password);
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}
