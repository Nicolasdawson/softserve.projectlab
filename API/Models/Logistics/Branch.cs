using API.Models.Logistics.Interfaces;

namespace API.Models.Logistics
{
    public class Branch : IBranch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }

        // Parameterless constructor for Dependency Injection (DI) & serialization
        public Branch() { }

        public Branch(int branchId, string name, string city, string region, string contactNumber)
        {
            BranchId = branchId;
            Name = name;
            City = city;
            Region = region;
            ContactNumber = contactNumber;
        }

        /// <summary>
        /// Adds a new branch.
        /// </summary>
        /// <param name="branch">The branch to add.</param>
        /// <returns>A result containing the added branch.</returns>
        public Result<IBranch> AddBranch(IBranch branch)
        {
            return Result<IBranch>.Success(branch);
        }

        /// <summary>
        /// Updates an existing branch.
        /// </summary>
        /// <param name="branch">The branch to update.</param>
        /// <returns>A result containing the updated branch.</returns>
        public Result<IBranch> UpdateBranch(IBranch branch)
        {
            return Result<IBranch>.Success(branch);
        }

        /// <summary>
        /// Retrieves a branch by its identifier.
        /// </summary>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns>A result containing the branch with the specified identifier.</returns>
        public Result<IBranch> GetBranchById(int branchId)
        {
            var branch = new Branch(branchId, "Main Branch", "New York", "NY", "555-1234");
            return Result<IBranch>.Success(branch);
        }

        /// <summary>
        /// Retrieves all branches.
        /// </summary>
        /// <returns>A result containing a list of all branches.</returns>
        public Result<List<IBranch>> GetAllBranches()
        {
            var branches = new List<IBranch>
                {
                    new Branch(1, "Main Branch", "New York", "NY", "555-1234"),
                    new Branch(2, "Secondary Branch", "Los Angeles", "CA", "555-5678")
                };
            return Result<List<IBranch>>.Success(branches);
        }

        /// <summary>
        /// Removes a branch by its identifier.
        /// </summary>
        /// <param name="branchId">The branch identifier.</param>
        /// <returns>A result indicating whether the branch was successfully removed.</returns>
        public Result<bool> RemoveBranch(int branchId)
        {
            return Result<bool>.Success(true);  // Assume the branch was removed successfully
        }
    }
}
