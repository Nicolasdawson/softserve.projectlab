using API.Implementations.Domain;
using API.Models.Logistics;
using API.Models;

namespace API.Services.Logistics
{
    public class BranchService : IBranchService
    {
        private readonly BranchDomain _branchDomain;

        public BranchService(BranchDomain branchDomain)
        {
            _branchDomain = branchDomain;
        }

        // Asynchronous method to add a branch
        public async Task<Result<Branch>> AddBranchAsync(Branch branch)
        {
            return await _branchDomain.CreateBranch(branch);
        }

        // Asynchronous method to update a branch
        public async Task<Result<Branch>> UpdateBranchAsync(Branch branch)
        {
            return await _branchDomain.UpdateBranch(branch);
        }

        // Asynchronous method to get a branch by ID
        public async Task<Result<Branch>> GetBranchByIdAsync(int branchId)
        {
            return await _branchDomain.GetBranchById(branchId);
        }

        // Asynchronous method to get all branches
        public async Task<Result<List<Branch>>> GetAllBranchesAsync()
        {
            return await _branchDomain.GetAllBranches();
        }

        // Asynchronous method to remove a branch
        public async Task<Result<bool>> RemoveBranchAsync(int branchId)
        {
            return await _branchDomain.RemoveBranch(branchId);
        }
    }
}
