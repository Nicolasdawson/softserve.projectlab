using API.Models.Logistics;
using API.Data.Entities;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;
using API.Data.Repositories.LogisticsRepositories.Interfaces;

namespace API.Implementations.Domain
{
    public class BranchDomain
    {
        private readonly IBranchRepository _branchRepository;

        public BranchDomain(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Result<Branch>> CreateBranchAsync(BranchDto branchDto)
        {
            try
            {
                // Check if a branch with the same name and city already exists
                var existingBranch = await _branchRepository.GetByNameAndCityAsync(branchDto.BranchName, branchDto.BranchCity);
                if (existingBranch != null)
                {
                    return Result<Branch>.Failure("A branch with the same name and city already exists.");
                }

                var branchEntity = new BranchEntity
                {
                    BranchName = branchDto.BranchName,
                    BranchCity = branchDto.BranchCity,
                    BranchRegion = branchDto.BranchRegion,
                    BranchContactNumber = branchDto.BranchContactNumber,
                    BranchContactEmail = branchDto.BranchContactEmail,
                    BranchAddress = branchDto.BranchAddress,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                await _branchRepository.AddAsync(branchEntity);

                var newBranchDto = new BranchDto
                {
                    BranchId = branchEntity.BranchId,
                    BranchName = branchEntity.BranchName,
                    BranchCity = branchEntity.BranchCity,
                    BranchRegion = branchEntity.BranchRegion,
                    BranchContactNumber = branchEntity.BranchContactNumber,
                    BranchContactEmail = branchEntity.BranchContactEmail,
                    BranchAddress = branchEntity.BranchAddress
                };

                var branch = new Branch(newBranchDto);

                return Result<Branch>.Success(branch);
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to create branch: {ex.Message}");
            }
        }

        public async Task<Result<Branch>> GetBranchById(int branchId)
        {
            try
            {
                var branchEntity = await _branchRepository.GetByIdAsync(branchId);
                if (branchEntity == null)
                    return Result<Branch>.Failure("Branch not found.");

                var branchDto = new BranchDto
                {
                    BranchId = branchEntity.BranchId,
                    BranchName = branchEntity.BranchName,
                    BranchCity = branchEntity.BranchCity,
                    BranchRegion = branchEntity.BranchRegion,
                    BranchContactNumber = branchEntity.BranchContactNumber,
                    BranchContactEmail = branchEntity.BranchContactEmail,
                    BranchAddress = branchEntity.BranchAddress
                };

                var branch = new Branch(branchDto);

                return Result<Branch>.Success(branch);
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
                var branchEntities = await _branchRepository.GetAllAsync();
                var branches = branchEntities.Select(branchEntity =>
                {
                    var branchDto = new BranchDto
                    {
                        BranchId = branchEntity.BranchId,
                        BranchName = branchEntity.BranchName,
                        BranchCity = branchEntity.BranchCity,
                        BranchRegion = branchEntity.BranchRegion,
                        BranchContactNumber = branchEntity.BranchContactNumber,
                        BranchContactEmail = branchEntity.BranchContactEmail,
                        BranchAddress = branchEntity.BranchAddress
                    };

                    return new Branch(branchDto);
                }).ToList();

                return Result<List<Branch>>.Success(branches);
            }
            catch (Exception ex)
            {
                return Result<List<Branch>>.Failure($"Failed to retrieve branches: {ex.Message}");
            }
        }

        public async Task<Result<Branch>> UpdateBranch(Branch branch)
        {
            try
            {
                var existingBranchEntity = await _branchRepository.GetByIdAsync(branch.GetBranchData().BranchId);
                if (existingBranchEntity == null)
                    return Result<Branch>.Failure("Branch not found.");

                var branchData = branch.GetBranchData();
                existingBranchEntity.BranchName = branchData.BranchName;
                existingBranchEntity.BranchCity = branchData.BranchCity;
                existingBranchEntity.BranchRegion = branchData.BranchRegion;
                existingBranchEntity.BranchContactNumber = branchData.BranchContactNumber;
                existingBranchEntity.BranchContactEmail = branchData.BranchContactEmail;
                existingBranchEntity.BranchAddress = branchData.BranchAddress;
                existingBranchEntity.UpdatedAt = DateTime.UtcNow;

                await _branchRepository.UpdateAsync(existingBranchEntity);

                var updatedBranch = new Branch(branchData);

                return Result<Branch>.Success(updatedBranch);
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to update branch: {ex.Message}");
            }
        }

        public async Task<Result<bool>> RemoveBranch(int branchId)
        {
            try
            {
                await _branchRepository.DeleteAsync(branchId);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove branch: {ex.Message}");
            }
        }
    }
}
