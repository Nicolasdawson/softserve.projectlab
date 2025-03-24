using API.Models.IntAdmin;
using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.IntAdmin
{
    /// <summary>
    /// API Controller for managing Permission operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// Constructor with dependency injection for IPermissionService.
        /// </summary>
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Adds a new permission.
        /// </summary>
        /// <param name="permission">Permission object to add</param>
        /// <returns>HTTP response with the created permission or error message</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddPermission([FromBody] Permission permission)
        {
            var result = await _permissionService.AddPermissionAsync(permission);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        /// <param name="permission">Permission object with updated data</param>
        /// <returns>HTTP response with the updated permission or error message</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePermission([FromBody] Permission permission)
        {
            var result = await _permissionService.UpdatePermissionAsync(permission);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves a permission by its unique ID.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission</param>
        /// <returns>HTTP response with the permission or error message</returns>
        [HttpGet("{permissionId}")]
        public async Task<IActionResult> GetPermissionById(int permissionId)
        {
            var result = await _permissionService.GetPermissionByIdAsync(permissionId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all permissions.
        /// </summary>
        /// <returns>HTTP response with the list of permissions or error message</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var result = await _permissionService.GetAllPermissionsAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a permission by its unique ID.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission to remove</param>
        /// <returns>HTTP response indicating success or failure</returns>
        [HttpDelete("remove/{permissionId}")]
        public async Task<IActionResult> RemovePermission(int permissionId)
        {
            var result = await _permissionService.RemovePermissionAsync(permissionId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
