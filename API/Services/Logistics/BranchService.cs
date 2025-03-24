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

        /// <summary>
        /// Asynchronously adds a new branch.
        /// </summary>
        /// <param name="branch">The branch to add.</param>
        /// <returns>A result containing the added branch.</returns>
        public async Task<Result<Branch>> AddBranchAsync(Branch branch)
        {
            return await _branchDomain.CreateBranch(branch);
        }

        /// <summary>
        /// Asynchronously updates an existing branch.
        /// </summary>
        /// <param name="branch">The branch to update.</param>
        /// <returns>A result containing the updated branch.</returns>
        public async Task<Result<Branch>> UpdateBranchAsync(Branch branch)
        {
            return await _branchDomain.UpdateBranch(branch);
        }

        /// <summary>
        /// Asynchronously retrieves a branch by its ID.
        /// </summary>
        /// <param name="branchId">The ID of the branch to retrieve.</param>
        /// <returns>A result containing the retrieved branch.</returns>
        public async Task<Result<Branch>> GetBranchByIdAsync(int branchId)
        {
            return await _branchDomain.GetBranchById(branchId);
        }

        /// <summary>
        /// Asynchronously retrieves all branches.
        /// </summary>
        /// <returns>A result containing a list of all branches.</returns>
        public async Task<Result<List<Branch>>> GetAllBranchesAsync()
        {
            return await _branchDomain.GetAllBranches();
        }

        /// <summary>
        /// Asynchronously removes a branch by its ID.
        /// </summary>
        /// <param name="branchId">The ID of the branch to remove.</param>
        /// <returns>A result indicating whether the removal was successful.</returns>
        public async Task<Result<bool>> RemoveBranchAsync(int branchId)
        {
            return await _branchDomain.RemoveBranch(branchId);
        }
    }
}
