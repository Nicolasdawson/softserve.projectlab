using API.Data.Entities;
using API.Models.IntAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    /// <summary>
    /// Domain class for handling User operations.
    /// Uses in-memory storage for users.
    /// </summary>
    public class UserDomain
    {
        // In-memory storage for users
        private readonly List<User> _users = new List<User>();

        /// <summary>
        /// Creates a new user and adds it to the in-memory list.
        /// </summary>
        public async Task<Result<User>> CreateUser(User user)
        {
            try
            {
                _users.Add(user);
                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Failed to create user: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing user if found.
        /// </summary>
        public async Task<Result<User>> UpdateUser(User user)
        {
            try
            {
                var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
                if (existingUser != null)
                {
                    // Update properties
                    existingUser.UserEmail = user.UserEmail;
                    existingUser.UserFirstName = user.UserFirstName;
                    existingUser.UserLastName = user.UserLastName;
                    existingUser.UserPhone = user.UserPhone;
                    existingUser.UserPassword = user.UserPassword;
                    existingUser.UserStatus = user.UserStatus;
                    existingUser.UserImage = user.UserImage;
                    existingUser.RoleId = user.RoleId;
                    existingUser.BranchId = user.BranchId;

                    return Result<User>.Success(existingUser);
                }
                else
                {
                    return Result<User>.Failure("User not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Failed to update user: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a user by its unique ID.
        /// </summary>
        public async Task<Result<User>> GetUserById(int userId)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.UserId == userId);
                return user != null
                    ? Result<User>.Success(user)
                    : Result<User>.Failure("User not found.");
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Failed to get user: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all users stored in memory.
        /// </summary>
        public async Task<Result<List<User>>> GetAllUsers()
        {
            try
            {
                return Result<List<User>>.Success(_users);
            }
            catch (Exception ex)
            {
                return Result<List<User>>.Failure($"Failed to retrieve users: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a user by its unique ID.
        /// </summary>
        public async Task<Result<bool>> RemoveUser(int userId)
        {
            try
            {
                var userToRemove = _users.FirstOrDefault(u => u.UserId == userId);
                if (userToRemove != null)
                {
                    _users.Remove(userToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("User not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove user: {ex.Message}");
            }
        }

        /// <summary>
        /// Authenticates a user by email and password.
        /// </summary>
        public async Task<Result<bool>> Authenticate(string email, string password)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.UserEmail == email && u.UserPassword == password);
                if (user != null)
                    return Result<bool>.Success(true);

                return Result<bool>.Failure("Invalid credentials");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to authenticate user: {ex.Message}");
            }
        }

        /// <summary>
        /// Assigns a role to a user if found.
        /// </summary>
        public async Task<Result<bool>> AssignRole(int userId, int roleId)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.RoleId = roleId;
                    return Result<bool>.Success(true);
                }
                return Result<bool>.Failure("User not found.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to assign role: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the password of a user if found.
        /// </summary>
        public async Task<Result<bool>> UpdatePassword(int userId, string newPassword)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.UserPassword = newPassword;
                    return Result<bool>.Success(true);
                }
                return Result<bool>.Failure("User not found.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to update password: {ex.Message}");
            }
        }
    }
}
