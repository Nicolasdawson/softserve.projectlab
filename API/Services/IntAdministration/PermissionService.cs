namespace API.Services.IntAdmin;

using AutoMapper;
using softserve.projectlabs.Shared.Utilities;
using API.Implementations.Domain;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.DTOs.Permission;

public class PermissionService : IPermissionService
{
    private readonly PermissionDomain _domain;
    private readonly IMapper _mapper;

    public PermissionService(PermissionDomain domain, IMapper mapper)
    {
        _domain = domain;
        _mapper = mapper;
    }

    public async Task<Result<PermissionDto>> CreatePermissionAsync(PermissionDto dto)
    {
        var modelResult = await _domain.CreatePermissionAsync(_mapper.Map<Permission>(dto));
        if (!modelResult.IsSuccess)
            return Result<PermissionDto>.Failure(modelResult.ErrorMessage);

        return Result<PermissionDto>.Success(
            _mapper.Map<PermissionDto>(modelResult.Data)
        );
    }

    public async Task<Result<PermissionDto>> UpdatePermissionAsync(PermissionDto dto)
    {
        var modelResult = await _domain.UpdatePermissionAsync(_mapper.Map<Permission>(dto));
        if (!modelResult.IsSuccess)
            return Result<PermissionDto>.Failure(modelResult.ErrorMessage);

        return Result<PermissionDto>.Success(
            _mapper.Map<PermissionDto>(modelResult.Data)
        );
    }

    public async Task<Result<PermissionDto>> GetPermissionByIdAsync(int permissionId)
    {
        var modelResult = await _domain.GetPermissionByIdAsync(permissionId);
        if (!modelResult.IsSuccess)
            return Result<PermissionDto>.Failure(modelResult.ErrorMessage);

        return Result<PermissionDto>.Success(
            _mapper.Map<PermissionDto>(modelResult.Data)
        );
    }

    public async Task<Result<List<PermissionDto>>> GetAllPermissionsAsync()
    {
        var modelResult = await _domain.GetAllPermissionsAsync();
        if (!modelResult.IsSuccess)
            return Result<List<PermissionDto>>.Failure(modelResult.ErrorMessage);

        return Result<List<PermissionDto>>.Success(
            _mapper.Map<List<PermissionDto>>(modelResult.Data)
        );
    }

    public async Task<Result<PermissionDto>> DeletePermissionAsync(int permissionId)
    {
        var modelResult = await _domain.DeletePermissionAsync(permissionId);
        if (!modelResult.IsSuccess)
            return Result<PermissionDto>.Failure(modelResult.ErrorMessage);

        return Result<PermissionDto>.Success(
            _mapper.Map<PermissionDto>(modelResult.Data)
        );
    }
}
