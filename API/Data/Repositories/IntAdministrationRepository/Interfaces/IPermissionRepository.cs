namespace API.Data.Repositories.IntAdministrationRepository.Interfaces;

using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPermissionRepository
{
    Task<Permission> CreateAsync(Permission permission);
    Task<Permission> UpdateAsync(Permission permission);
    Task<Permission> GetByIdAsync(int permissionId);
    Task<List<Permission>> GetAllAsync();
    Task<Permission> DeleteAsync(int permissionId);
}
