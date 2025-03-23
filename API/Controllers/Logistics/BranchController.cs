using API.Models.Logistics;
using API.Services.Logistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Controllers.Logistics
{
    /// <summary>
    /// Controller for managing branches.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BranchController"/> class.
        /// </summary>
        /// <param name="branchService">The branch service.</param>
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        /// <summary>
        /// Adds a new branch.
        /// </summary>
        /// <param name="branch">The branch to add.</param>
        /// <returns>The result of the add operation.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddBranch([FromBody] Branch branch)
        {
            var result = await _branchService.AddBranchAsync(branch);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing branch.
        /// </summary>
        /// <param name="branch">The branch to update.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBranch([FromBody] Branch branch)
        {
            var result = await _branchService.UpdateBranchAsync(branch);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Gets a branch by its ID.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch with the specified ID.</returns>
        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetBranchById(int branchId)
        {
            var result = await _branchService.GetBranchByIdAsync(branchId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Gets all branches.
        /// </summary>
        /// <returns>A list of all branches.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBranches()
        {
            var result = await _branchService.GetAllBranchesAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a branch by its ID.
        /// </summary>
        /// <param name="branchId">The ID of the branch to remove.</param>
        /// <returns>The result of the remove operation.</returns>
        [HttpDelete("remove/{branchId}")]
        public async Task<IActionResult> RemoveBranch(int branchId)
        {
            var result = await _branchService.RemoveBranchAsync(branchId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
