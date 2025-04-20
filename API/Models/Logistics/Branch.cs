using API.Models.Logistics.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;

namespace API.Models.Logistics
{
    public class Branch : IBranch
    {
        private readonly BranchDto _branchDto;

        public Branch(BranchDto branchDto)
        {
            _branchDto = branchDto;
        }

        public BranchDto GetBranchData() => _branchDto;

        public Result<IBranch> AddBranch(IBranch branch)
        {
            return Result<IBranch>.Success(branch);
        }

        public Result<IBranch> UpdateBranch(IBranch branch)
        {
            return Result<IBranch>.Success(branch);
        }

        public Result<IBranch> GetBranchById(int branchId)
        {
            var branch = new Branch(new BranchDto
            {
                BranchId = branchId,
                BranchName = "Main Branch",
                BranchCity = "New York",
                BranchRegion = "NY",
                BranchContactNumber = "555-1234"
            });
            return Result<IBranch>.Success(branch);
        }

        public Result<List<IBranch>> GetAllBranches()
        {
            var branches = new List<IBranch>
            {
                new Branch(new BranchDto
                {
                    BranchId = 1,
                    BranchName = "Main Branch",
                    BranchCity = "New York",
                    BranchRegion = "NY",
                    BranchContactNumber = "555-1234"
                }),
                new Branch(new BranchDto
                {
                    BranchId = 2,
                    BranchName = "Secondary Branch",
                    BranchCity = "Los Angeles",
                    BranchRegion = "CA",
                    BranchContactNumber = "555-5678"
                })
            };
            return Result<List<IBranch>>.Success(branches);
        }

        public Result<bool> RemoveBranch(int branchId)
        {
            return Result<bool>.Success(true); 
        }
    }
}
