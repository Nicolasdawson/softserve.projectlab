using API.Models.IntAdmin;
using API.Models.IntAdmin.Interfaces;
using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.IntAdmin
{
    /// <summary>
    /// API Controller for managing Role operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// Constructor with dependency injection for IRoleService.
        /// </summary>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Adds a new role.
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            var result = await _roleService.AddRoleAsync(role);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            var result = await _roleService.UpdateRoleAsync(role);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves a role by its unique ID.
        /// </summary>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var result = await _roleService.GetRoleByIdAsync(roleId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a role by its unique ID.
        /// </summary>
        [HttpDelete("remove/{roleId}")]
        public async Task<IActionResult> RemoveRole(int roleId)
        {
            var result = await _roleService.RemoveRoleAsync(roleId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Adds a permission to the role's Permissions list.
        /// </summary>
        [HttpPost("{roleId}/add-permission")]
        public async Task<IActionResult> AddPermissionToRole(int roleId, [FromBody] Permission permission)
        {
            var result = await _roleService.AddPermissionToRoleAsync(roleId, permission);
            return result.IsSuccess ? Ok("Permission added successfully.") : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a permission from the role's Permissions list by permission ID.
        /// </summary>
        [HttpDelete("{roleId}/remove-permission/{permissionId}")]
        public async Task<IActionResult> RemovePermissionFromRole(int roleId, int permissionId)
        {
            var result = await _roleService.RemovePermissionFromRoleAsync(roleId, permissionId);
            return result.IsSuccess ? Ok("Permission removed successfully.") : NotFound(result.ErrorMessage);
        }
    }
}
