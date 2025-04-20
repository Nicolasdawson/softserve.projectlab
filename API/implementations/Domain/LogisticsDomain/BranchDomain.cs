using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using softserve.projectlabs.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using API.Data;
using softserve.projectlabs.Shared.DTOs;

namespace API.Implementations.Domain
{
    public class BranchDomain
    {
        private readonly ApplicationDbContext _context;

        public BranchDomain(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Branch>> CreateBranchAsync(BranchDto branchDto)
        {
            try
            {
                // Map BranchDto to BranchEntity
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

                // Add the new branch entity to the database
                _context.BranchEntities.Add(branchEntity);
                await _context.SaveChangesAsync();

                // Map BranchEntity to BranchDto
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

                // Create a new Branch instance
                var branch = new Branch(newBranchDto);

                return Result<Branch>.Success(branch);
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to create branch: {ex.Message}");
            }
        }

        public async Task<BranchEntity?> GetBranchByNameAndCityAsync(string name, string city)
        {
            return await _context.BranchEntities
                .FirstOrDefaultAsync(b => b.BranchName == name && b.BranchCity == city && !b.IsDeleted);
        }

        public async Task<Result<Branch>> UpdateBranch(Branch branch)
        {
            try
            {
                var existingBranchEntity = await _context.BranchEntities
                    .FirstOrDefaultAsync(b => b.BranchId == branch.GetBranchData().BranchId && !b.IsDeleted);

                if (existingBranchEntity == null)
                {
                    return Result<Branch>.Failure("Branch not found.");
                }

                // Map BranchDto to BranchEntity
                var branchData = branch.GetBranchData();
                existingBranchEntity.BranchName = branchData.BranchName;
                existingBranchEntity.BranchCity = branchData.BranchCity;
                existingBranchEntity.BranchRegion = branchData.BranchRegion;
                existingBranchEntity.BranchContactNumber = branchData.BranchContactNumber;
                existingBranchEntity.BranchContactEmail = branchData.BranchContactEmail;
                existingBranchEntity.BranchAddress = branchData.BranchAddress;
                existingBranchEntity.UpdatedAt = DateTime.UtcNow;

                _context.BranchEntities.Update(existingBranchEntity);
                await _context.SaveChangesAsync();

                // Create a new Branch instance
                var updatedBranch = new Branch(branchData);

                return Result<Branch>.Success(updatedBranch);
            }
            catch (Exception ex)
            {
                return Result<Branch>.Failure($"Failed to update branch: {ex.Message}");
            }
        }

        public async Task<Result<Branch>> GetBranchById(int branchId)
        {
            try
            {
                var branchEntity = await _context.BranchEntities
                    .FirstOrDefaultAsync(b => b.BranchId == branchId && !b.IsDeleted);

                if (branchEntity == null)
                    return Result<Branch>.Failure("Branch not found.");

                // Map BranchEntity to BranchDto
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

                // Create a new Branch instance
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
                var branchEntities = await _context.BranchEntities
                    .Where(b => !b.IsDeleted)
                    .ToListAsync();

                // Map List<BranchEntity> to List<Branch>
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

        public async Task<Result<bool>> RemoveBranch(int branchId)
        {
            try
            {
                var branchEntity = await _context.BranchEntities
                    .FirstOrDefaultAsync(b => b.BranchId == branchId);

                if (branchEntity == null)
                    return Result<bool>.Failure("Branch not found.");

                branchEntity.IsDeleted = true;
                branchEntity.UpdatedAt = DateTime.UtcNow;

                _context.BranchEntities.Update(branchEntity);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove branch: {ex.Message}");
            }
        }
    }
}
