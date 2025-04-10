using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service interface for user operations.
    /// </summary>
    public interface IUserService
    {
        Task<Result<User>> AddUserAsync(User user);
        Task<Result<User>> UpdateUserAsync(User user);
        Task<Result<User>> GetUserByIdAsync(int userId);
        Task<Result<List<User>>> GetAllUsersAsync();
        Task<Result<bool>> RemoveUserAsync(int userId);

        // Additional methods
        Task<Result<bool>> AuthenticateAsync(string email, string password);
        Task<Result<bool>> AssignRoleAsync(int userId, int roleId);
        Task<Result<bool>> UpdatePasswordAsync(int userId, string newPassword);
    }
}
