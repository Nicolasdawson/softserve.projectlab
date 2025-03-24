using API.Models.IntAdmin;
using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.IntAdmin
{
    /// <summary>
    /// API Controller for managing User operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor with dependency injection for IUserService.
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var result = await _userService.AddUserAsync(user);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var result = await _userService.UpdateUserAsync(user);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves a user by its unique ID.
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a user by its unique ID.
        /// </summary>
        [HttpDelete("remove/{userId}")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var result = await _userService.RemoveUserAsync(userId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Authenticates a user by email and password.
        /// </summary>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var result = await _userService.AuthenticateAsync(request.Email, request.Password);
            return result.IsSuccess ? Ok("Authenticated successfully") : Unauthorized(result.ErrorMessage);
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        [HttpPut("assign-role/{userId}/{roleId}")]
        public async Task<IActionResult> AssignRole(int userId, int roleId)
        {
            var result = await _userService.AssignRoleAsync(userId, roleId);
            return result.IsSuccess ? Ok("Role assigned successfully") : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Updates the user's password.
        /// </summary>
        [HttpPut("update-password/{userId}")]
        public async Task<IActionResult> UpdatePassword(int userId, [FromBody] PasswordRequest request)
        {
            var result = await _userService.UpdatePasswordAsync(userId, request.NewPassword);
            return result.IsSuccess ? Ok("Password updated successfully") : NotFound(result.ErrorMessage);
        }
    }

    /// <summary>
    /// DTO for login requests.
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// DTO for password update requests.
    /// </summary>
    public class PasswordRequest
    {
        public string NewPassword { get; set; }
    }
}
