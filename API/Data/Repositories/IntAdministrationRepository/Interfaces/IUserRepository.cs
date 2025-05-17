using API.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories.IntAdministrationRepository.Interfaces;

public interface IUserRepository
{
    Task<UserEntity?> GetByIdAsync(int userId);
    Task<List<UserEntity>> GetAllAsync();
    Task<UserEntity> AddAsync(UserEntity entity);
    Task<UserEntity> UpdateAsync(UserEntity entity);
    Task DeleteAsync(int userId);
    Task<bool> ExistsAsync(int userId);

    // Métodos para la tabla pivote
    Task<List<UserRoleEntity>> GetUserRolesAsync(int userId);
    Task AddUserRoleAsync(UserRoleEntity pivot);
    Task RemoveUserRoleAsync(UserRoleEntity pivot);

    Task<UserEntity?> GetByEmailAsync(string email);
}
