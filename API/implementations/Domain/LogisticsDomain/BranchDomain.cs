using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using softserve.projectlabs.Shared.Utilities;
namespace API.Implementations.Domain
{
    public class BranchDomain
    {
        private readonly List<Branch> _branches = new List<Branch>(); // Example: In-memory storage for branches

       
        public async Task<Result<Branch>> CreateBranch(Branch branch)
        {
            try
            {
                _branches.Add(branch);
                return Result<Branch>.Success(branch);
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to create branch: {ex.Message}");
            }
        }

        public async Task<Result<Branch>> UpdateBranch(Branch branch)
        {
            try
            {
                var existingBranch = _branches.FirstOrDefault(b => b.BranchId == branch.BranchId);
                if (existingBranch != null)
                {
                    existingBranch.Name = branch.Name;
                    return Result<Branch>.Success(existingBranch);
                }
                else
                {
                    return Result<Branch>.Failure("Branch not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to update branch: {ex.Message}");
            }
        }

    
        public async Task<Result<Branch>> GetBranchById(int branchId)
        {
            try
            {
                var branch = _branches.FirstOrDefault(b => b.BranchId == branchId);
                return branch != null ? Result<Branch>.Success(branch) : Result<Branch>.Failure("Branch not found.");
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to get branch: {ex.Message}");
            }
        }

        public async Task<Result<List<Branch>>> GetAllBranches()
        {
            try
            {
                return Result<List<Branch>>.Success(_branches);
            }
            catch (Exception ex)
            {
                return Result<List<Branch>>.Failure($"Failed to retrieve branches: {ex.Message}");
            }
        }

       
        public async Task<Result<bool>> RemoveBranch(int branchId)
        {
            try
            {
                var branchToRemove = _branches.FirstOrDefault(b => b.BranchId == branchId);
                if (branchToRemove != null)
                {
                    _branches.Remove(branchToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Branch not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove branch: {ex.Message}");
            }
        }
    }
}
