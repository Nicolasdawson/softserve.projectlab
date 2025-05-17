using Microsoft.AspNetCore.Mvc;
using softserve.projectlabs.Shared.DTOs.User;
using API.Services.IntAdmin;

namespace API.Controllers.IntAdmin;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateDto userDto)
    {
        var result = await _authService.RegisterAsync(userDto);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return result.IsSuccess ? Ok(result.Data) : Unauthorized(result.ErrorMessage);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken);
        return result.IsSuccess ? Ok(result.Data) : Unauthorized(result.ErrorMessage);
    }
}
