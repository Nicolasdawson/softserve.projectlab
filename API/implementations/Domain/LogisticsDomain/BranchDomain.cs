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
                // Manually map BranchDto to BranchEntity
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

                // Manually map BranchEntity to Branch
                var branch = new Branch
                {
                    BranchId = branchEntity.BranchId,
                    Name = branchEntity.BranchName,
                    City = branchEntity.BranchCity,
                    Region = branchEntity.BranchRegion,
                    ContactNumber = branchEntity.BranchContactNumber,
                    ContactEmail = branchEntity.BranchContactEmail,
                    Address = branchEntity.BranchAddress
                };

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
                    .FirstOrDefaultAsync(b => b.BranchId == branch.BranchId && !b.IsDeleted);

                if (existingBranchEntity == null)
                {
                    return Result<Branch>.Failure("Branch not found.");
                }

                // Manually map Branch to BranchEntity
                existingBranchEntity.BranchName = branch.Name;
                existingBranchEntity.BranchCity = branch.City;
                existingBranchEntity.BranchRegion = branch.Region;
                existingBranchEntity.BranchContactNumber = branch.ContactNumber;
                existingBranchEntity.BranchContactEmail = branch.ContactEmail;
                existingBranchEntity.BranchAddress = branch.Address;
                existingBranchEntity.UpdatedAt = DateTime.UtcNow;

                _context.BranchEntities.Update(existingBranchEntity);
                await _context.SaveChangesAsync();

                // Manually map BranchEntity to Branch
                var updatedBranch = new Branch
                {
                    BranchId = existingBranchEntity.BranchId,
                    Name = existingBranchEntity.BranchName,
                    City = existingBranchEntity.BranchCity,
                    Region = existingBranchEntity.BranchRegion,
                    ContactNumber = existingBranchEntity.BranchContactNumber,
                    ContactEmail = existingBranchEntity.BranchContactEmail,
                    Address = existingBranchEntity.BranchAddress
                };

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

                // Manually map BranchEntity to Branch
                var branch = new Branch
                {
                    BranchId = branchEntity.BranchId,
                    Name = branchEntity.BranchName,
                    City = branchEntity.BranchCity,
                    Region = branchEntity.BranchRegion,
                    ContactNumber = branchEntity.BranchContactNumber,
                    ContactEmail = branchEntity.BranchContactEmail,
                    Address = branchEntity.BranchAddress
                };

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

                // Manually map List<BranchEntity> to List<Branch>
                var branches = branchEntities.Select(branchEntity => new Branch
                {
                    BranchId = branchEntity.BranchId,
                    Name = branchEntity.BranchName,
                    City = branchEntity.BranchCity,
                    Region = branchEntity.BranchRegion,
                    ContactNumber = branchEntity.BranchContactNumber,
                    ContactEmail = branchEntity.BranchContactEmail,
                    Address = branchEntity.BranchAddress
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
