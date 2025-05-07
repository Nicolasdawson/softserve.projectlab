using softserve.projectlabs.Shared.Utilities;

namespace API.Models.Logistics.Interfaces
{
    public interface IBranch
    {
        // Business logic methods
        Result<IBranch> AddBranch(IBranch branch);
        Result<IBranch> UpdateBranch(IBranch branch);
        Result<IBranch> GetBranchById(int branchId);
        Result<List<IBranch>> GetAllBranches();
        Result<bool> RemoveBranch(int branchId);
    }
}
