using API.Models.Logistics.Interfaces;

namespace API.Models.Logistics
{
    public class Branch : IBranch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }  // New attribute for City
        public string Region { get; set; }  // New attribute for Region
        public string ContactNumber { get; set; }

        // Constructor with City and Region
        public Branch(int branchId, string name, string city, string region, string contactNumber)
        {
            BranchId = branchId;
            Name = name;
            City = city;
            Region = region;
            ContactNumber = contactNumber;
        }

        // Logic for adding a new branch (e.g., saving to a database or collection)
        public Result<IBranch> AddBranch(IBranch branch)
        {
            return Result<IBranch>.Success(branch);
        }

        // Logic for updating an existing branch
        public Result<IBranch> UpdateBranch(IBranch branch)
        {
            return Result<IBranch>.Success(branch);
        }

        // Logic for retrieving a branch by its ID
        public Result<IBranch> GetBranchById(int branchId)
        {
            var branch = new Branch(branchId, "Main Branch", "New York", "NY", "555-1234");
            return Result<IBranch>.Success(branch);
        }

        // Logic for retrieving all branches
        public Result<List<IBranch>> GetAllBranches()
        {
            var branches = new List<IBranch>
            {
                new Branch(1, "Main Branch", "New York", "NY", "555-1234"),
                new Branch(2, "Secondary Branch", "Los Angeles", "CA", "555-5678")
            };
            return Result<List<IBranch>>.Success(branches);
        }

        // Logic for removing a branch
        public Result<bool> RemoveBranch(int branchId)
        {
            return Result<bool>.Success(true);  // Assume the branch was removed successfully
        }
    }
}
