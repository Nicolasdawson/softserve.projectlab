using API.Models.Logistics;
using API.Services.Logistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper;

namespace API.Controllers.Logistics
{
    
    [ApiController]
    [Route("api/branches")] 
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper; 

        public BranchController(IBranchService branchService, IMapper mapper)
        {
            _branchService = branchService;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Branch, BranchDto>()).CreateMapper(); 
        }


        [HttpPost]
        public async Task<IActionResult> AddBranch([FromBody] Branch branch)
        {
            // Map Branch to BranchDto
            var branchDto = _mapper.Map<BranchDto>(branch);

            var result = await _branchService.AddBranchAsync(branchDto);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }


        [HttpPut("{branchId}")]
        public async Task<IActionResult> UpdateBranch(int branchId, [FromBody] Branch branch)
        {
            branch.BranchId = branchId; 

            // Map Branch to BranchDto
            var branchDto = _mapper.Map<BranchDto>(branch);

            var result = await _branchService.UpdateBranchAsync(branchDto);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Gets a branch by its ID.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch with the specified ID.</returns>
        [HttpGet("{branchId}")] // Already correctly RESTful
        public async Task<IActionResult> GetBranchById(int branchId)
        {
            var result = await _branchService.GetBranchByIdAsync(branchId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Gets all branches.
        /// </summary>
        /// <returns>A list of all branches.</returns>
        [HttpGet] // Remove "all" since GET /api/branches implies fetching all
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
        [HttpDelete("{branchId}")] // RESTful: DELETE method with resource ID
        public async Task<IActionResult> RemoveBranch(int branchId)
        {
            var result = await _branchService.RemoveBranchAsync(branchId);
            //return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
            if (result.IsNoContent)
            {
                return NoContent();  // Returns HTTP 204 No Content
            }
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
