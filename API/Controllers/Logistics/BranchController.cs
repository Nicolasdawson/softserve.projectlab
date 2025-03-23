using API.Models.Logistics;
using API.Services.Logistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Controllers.Logistics
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBranch([FromBody] Branch branch)
        {
            var result = await _branchService.AddBranchAsync(branch);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBranch([FromBody] Branch branch)
        {
            var result = await _branchService.UpdateBranchAsync(branch);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetBranchById(int branchId)
        {
            var result = await _branchService.GetBranchByIdAsync(branchId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBranches()
        {
            var result = await _branchService.GetAllBranchesAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("remove/{branchId}")]
        public async Task<IActionResult> RemoveBranch(int branchId)
        {
            var result = await _branchService.RemoveBranchAsync(branchId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
