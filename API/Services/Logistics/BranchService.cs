using API.Implementations.Domain;
using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper; 

namespace API.Services.Logistics
{
    public class BranchService : IBranchService
    {
        private readonly BranchDomain _branchDomain;
        private readonly IMapper _mapper; 

        public BranchService(BranchDomain branchDomain, IMapper mapper)
        {
            _branchDomain = branchDomain;
            _mapper = mapper;
        }

        public async Task<Result<BranchDto>> AddBranchAsync(BranchDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto); 
            var result = await _branchDomain.CreateBranch(branch);
            return result.IsSuccess
                ? Result<BranchDto>.Success(_mapper.Map<BranchDto>(result.Data)) 
                : Result<BranchDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<BranchDto>> UpdateBranchAsync(BranchDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto); 
            var result = await _branchDomain.UpdateBranch(branch);
            return result.IsSuccess
                ? Result<BranchDto>.Success(_mapper.Map<BranchDto>(result.Data)) // Map entity back to DTO
                : Result<BranchDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<BranchDto>> GetBranchByIdAsync(int branchId)
        {
            var result = await _branchDomain.GetBranchById(branchId);
            return result.IsSuccess
                ? Result<BranchDto>.Success(_mapper.Map<BranchDto>(result.Data)) // Map entity to DTO
                : Result<BranchDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<BranchDto>>> GetAllBranchesAsync()
        {
            var result = await _branchDomain.GetAllBranches();
            return result.IsSuccess
                ? Result<List<BranchDto>>.Success(_mapper.Map<List<BranchDto>>(result.Data)) // Map list of entities to DTOs
                : Result<List<BranchDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> RemoveBranchAsync(int branchId)
        {
            return await _branchDomain.RemoveBranch(branchId);
        }
    }
}
