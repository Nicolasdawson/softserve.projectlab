using API.Data.Entities;
using API.Implementations.Domain;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using softserve.projectlabs.Shared.Utilities;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service class for user operations. Delegates business logic to the UserDomain.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserDomain _userDomain;

        /// <summary>
        /// Constructor with dependency injection for UserDomain.
        /// </summary>
        public UserService(UserDomain userDomain)
        {
            _userDomain = userDomain;
        }

        public async Task<Result<User>> AddUserAsync(User user)
        {
            return await _userDomain.CreateUser(user);
        }

        public async Task<Result<User>> UpdateUserAsync(User user)
        {
            return await _userDomain.UpdateUser(user);
        }

        public async Task<Result<User>> GetUserByIdAsync(int userId)
        {
            return await _userDomain.GetUserById(userId);
        }

        public async Task<Result<List<User>>> GetAllUsersAsync()
        {
            return await _userDomain.GetAllUsers();
        }

        public async Task<Result<bool>> RemoveUserAsync(int userId)
        {
            return await _userDomain.RemoveUser(userId);
        }

        public async Task<Result<bool>> AuthenticateAsync(string email, string password)
        {
            return await _userDomain.Authenticate(email, password);
        }

        public async Task<Result<bool>> AssignRoleAsync(int userId, int roleId)
        {
            return await _userDomain.AssignRole(userId, roleId);
        }

        public async Task<Result<bool>> UpdatePasswordAsync(int userId, string newPassword)
        {
            return await _userDomain.UpdatePassword(userId, newPassword);
        }
    }
}
