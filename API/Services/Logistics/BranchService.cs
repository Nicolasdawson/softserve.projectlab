using API.Implementations.Domain;
using API.Models.Logistics;
using API.Data.Entities;
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

        public async Task<Result<Branch>> AddBranchAsync(Branch branch)
        {
            return await _branchDomain.CreateBranch(branch);
        }

        public async Task<Result<Branch>> UpdateBranchAsync(Branch branch)
        {
            return await _branchDomain.UpdateBranch(branch);
        }

        public async Task<Result<Branch>> GetBranchByIdAsync(int branchId)
        {
            return await _branchDomain.GetBranchById(branchId);
        }

        public async Task<Result<List<Branch>>> GetAllBranchesAsync()
        {
            return await _branchDomain.GetAllBranches();
        }

        public async Task<Result<bool>> RemoveBranchAsync(int branchId)
        {
            return await _branchDomain.RemoveBranch(branchId);
        }
    }
}
