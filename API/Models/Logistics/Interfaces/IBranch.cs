namespace API.Models.Logistics.Interfaces
{
    public interface IBranch
    {
        int BranchId { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        string ContactNumber { get; set; }

        Result<IBranch> AddBranch(IBranch branch);
        Result<IBranch> UpdateBranch(IBranch branch);
        Result<IBranch> GetBranchById(int branchId);
        Result<List<IBranch>> GetAllBranches();
        Result<bool> RemoveBranch(int branchId);
    }
}
