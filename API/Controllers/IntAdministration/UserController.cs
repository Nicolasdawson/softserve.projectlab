using Microsoft.AspNetCore.Mvc;
using API.Services.IntAdmin;
using softserve.projectlabs.Shared.DTOs.User;

namespace API.Controllers.IntAdmin;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var result = await _userService.GetUserByIdAsync(userId);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto userDto)
    {
        var result = await _userService.UpdateUserAsync(userId, userDto);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
    }

    [HttpPost("{userId}/assign-roles")]
    public async Task<IActionResult> AssignRolesToUser(int userId, [FromBody] List<int> roleIds)
    {
        var result = await _userService.AssignRolesAsync(userId, roleIds);
        return result.IsSuccess ? Ok("Roles assigned successfully.") : BadRequest(result.ErrorMessage);
    }

    [HttpPut("{userId}/update-password")]
    public async Task<IActionResult> UpdatePassword(int userId, [FromBody] string newPassword)
    {
        var result = await _userService.UpdatePasswordAsync(userId, newPassword);
        return result.IsSuccess ? Ok("Password updated") : BadRequest(result.ErrorMessage);
    }
}
