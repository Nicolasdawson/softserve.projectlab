using API.Implementations.Domain;
using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;

namespace API.Services.Logistics
{
    public class BranchService : IBranchService
    {
        private readonly BranchDomain _branchDomain;

        public BranchService(BranchDomain branchDomain)
        {
            _branchDomain = branchDomain ?? throw new ArgumentNullException(nameof(branchDomain));
        }

        public async Task<Result<BranchDto>> AddBranchAsync(BranchDto branchDto)
        {
            var existingBranch = await _branchDomain.GetBranchByNameAndCityAsync(branchDto.BranchName, branchDto.BranchCity);
            if (existingBranch != null)
            {
                return Result<BranchDto>.Failure($"A branch with the name '{branchDto.BranchName}' already exists in the city '{branchDto.BranchCity}'.");
            }

            var result = await _branchDomain.CreateBranchAsync(branchDto);

            if (!result.IsSuccess)
                return Result<BranchDto>.Failure(result.ErrorMessage, result.ErrorCode);

            // Use GetBranchData() to retrieve BranchDto
            var branchData = result.Data.GetBranchData();

            return Result<BranchDto>.Success(branchData);
        }


        public async Task<Result<BranchDto>> UpdateBranchAsync(BranchDto branchDto)
        {
            // Create a new Branch instance using BranchDto
            var branch = new Branch(branchDto);

            var result = await _branchDomain.UpdateBranch(branch);

            if (!result.IsSuccess)
                return Result<BranchDto>.Failure(result.ErrorMessage);

            // Use GetBranchData() to retrieve BranchDto
            var updatedBranchData = result.Data.GetBranchData();

            return Result<BranchDto>.Success(updatedBranchData);
        }


        public async Task<Result<BranchDto>> GetBranchByIdAsync(int branchId)
        {
            var result = await _branchDomain.GetBranchById(branchId);

            if (!result.IsSuccess)
                return Result<BranchDto>.Failure(result.ErrorMessage);

            // Use GetBranchData() to retrieve BranchDto
            var branchData = result.Data.GetBranchData();

            return Result<BranchDto>.Success(branchData);
        }


        public async Task<Result<List<BranchDto>>> GetAllBranchesAsync()
        {
            var result = await _branchDomain.GetAllBranches();

            if (!result.IsSuccess)
                return Result<List<BranchDto>>.Failure(result.ErrorMessage);

            // Use GetBranchData() to map List<Branch> to List<BranchDto>
            var branchDtos = result.Data.Select(branch => branch.GetBranchData()).ToList();

            return Result<List<BranchDto>>.Success(branchDtos);
        }


        public async Task<Result<bool>> RemoveBranchAsync(int branchId)
        {
            return await _branchDomain.RemoveBranch(branchId);
        }
    }
}
