using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using API.Data.Entities;

namespace API.Services.Logistics
{
    public interface IBranchService
    {
        Task<Result<Branch>> AddBranchAsync(Branch branch); // Updated to async
        Task<Result<Branch>> UpdateBranchAsync(Branch branch); // Updated to async
        Task<Result<Branch>> GetBranchByIdAsync(int branchId); // Updated to async
        Task<Result<List<Branch>>> GetAllBranchesAsync(); // Updated to async
        Task<Result<bool>> RemoveBranchAsync(int branchId); // Updated to async
    }
}
