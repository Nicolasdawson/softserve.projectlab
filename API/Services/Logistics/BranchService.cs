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

            // Manually map Branch to BranchDto
            var branchDtoResult = new BranchDto
            {
                BranchId = result.Data.BranchId,
                BranchName = result.Data.Name,
                BranchCity = result.Data.City,
                BranchAddress = result.Data.Address,
                BranchRegion = result.Data.Region,
                BranchContactNumber = result.Data.ContactNumber,
                BranchContactEmail = result.Data.ContactEmail
            };

            return Result<BranchDto>.Success(branchDtoResult);
        }

        public async Task<Result<BranchDto>> UpdateBranchAsync(BranchDto branchDto)
        {
            // Manually map BranchDto to Branch
            var branch = new Branch
            {
                BranchId = branchDto.BranchId,
                Name = branchDto.BranchName,
                City = branchDto.BranchCity,
                Address = branchDto.BranchAddress,
                Region = branchDto.BranchRegion,
                ContactNumber = branchDto.BranchContactNumber,
                ContactEmail = branchDto.BranchContactEmail
            };

            var result = await _branchDomain.UpdateBranch(branch);

            if (!result.IsSuccess)
                return Result<BranchDto>.Failure(result.ErrorMessage);

            // Manually map Branch to BranchDto
            var updatedBranchDto = new BranchDto
            {
                BranchId = result.Data.BranchId,
                BranchName = result.Data.Name,
                BranchCity = result.Data.City,
                BranchAddress = result.Data.Address,
                BranchRegion = result.Data.Region,
                BranchContactNumber = result.Data.ContactNumber,
                BranchContactEmail = result.Data.ContactEmail
            };

            return Result<BranchDto>.Success(updatedBranchDto);
        }

        public async Task<Result<BranchDto>> GetBranchByIdAsync(int branchId)
        {
            var result = await _branchDomain.GetBranchById(branchId);

            if (!result.IsSuccess)
                return Result<BranchDto>.Failure(result.ErrorMessage);

            // Manually map Branch to BranchDto
            var branchDto = new BranchDto
            {
                BranchId = result.Data.BranchId,
                BranchName = result.Data.Name,
                BranchCity = result.Data.City,
                BranchAddress = result.Data.Address,
                BranchRegion = result.Data.Region,
                BranchContactNumber = result.Data.ContactNumber,
                BranchContactEmail = result.Data.ContactEmail
            };

            return Result<BranchDto>.Success(branchDto);
        }

        public async Task<Result<List<BranchDto>>> GetAllBranchesAsync()
        {
            var result = await _branchDomain.GetAllBranches();

            if (!result.IsSuccess)
                return Result<List<BranchDto>>.Failure(result.ErrorMessage);

            // Manually map List<Branch> to List<BranchDto>
            var branchDtos = result.Data.Select(branch => new BranchDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.Name,
                BranchCity = branch.City,
                BranchAddress = branch.Address,
                BranchRegion = branch.Region,
                BranchContactNumber = branch.ContactNumber,
                BranchContactEmail = branch.ContactEmail
            }).ToList();

            return Result<List<BranchDto>>.Success(branchDtos);
        }

        public async Task<Result<bool>> RemoveBranchAsync(int branchId)
        {
            return await _branchDomain.RemoveBranch(branchId);
        }
    }
}
