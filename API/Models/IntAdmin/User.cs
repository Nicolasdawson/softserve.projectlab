using API.Data.Entities;
using API.Models.IntAdmin.Interfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string UserStatus { get; set; }
        public string UserImage { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }

        public User(int userId, string userEmail, string userFirstName, string userLastName,
                    string userPhone, string userPassword, string userStatus,
                    int roleId, int branchId, string userImage)
        {
            UserId = userId;
            UserEmail = userEmail;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            UserPhone = userPhone;
            UserPassword = userPassword;
            UserStatus = userStatus;
            RoleId = roleId;
            BranchId = branchId;
            UserImage = userImage;
        }

        /// <summary>
        /// Default constructor for serialization or other purposes.
        /// </summary>
        public User() { }

        // CRUD implementations (examples)

        /// <summary>
        /// Logic to add a new user.
        /// </summary>
        /// <param name="user">User object to add</param>
        /// <returns>Result with the created user</returns>
        public Result<IUser> AddUser(IUser user)
        {
            // TODO: Adding the logic to add the user to the data source
            return Result<IUser>.Success(user);
        }

        /// <summary>
        /// Logic to update an existing user.
        /// </summary>
        /// <param name="user">User object with updated data</param>
        /// <returns>Result with the updated user</returns>
        public Result<IUser> UpdateUser(IUser user)
        {
            // TODO: Adding the logic to update the user in the data source
            return Result<IUser>.Success(user);
        }

        /// <summary>
        /// Logic to get a user by its ID.
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>Result with the requested user</returns>
        public Result<IUser> GetUserById(int userId)
        {
            // TODO: Fetch the user from the data source
            var user = new User(
                userId,
                "user@example.com",
                "Nombre",
                "Apellido",
                "555-1234",
                "password",
                "Activo",
                1,
                101,
                "imagen.png"
            );
            return Result<IUser>.Success(user);
        }

        /// <summary>
        /// Logic to get all users.
        /// </summary>
        /// <returns>Result with a list of users</returns>
        public Result<List<IUser>> GetAllUsers()
        {
            // TODO: Fetch all users from the data source
            var users = new List<IUser>
            {
                new User(
                    1,
                    "user1@example.com",
                    "Usuario",
                    "Uno",
                    "555-1111",
                    "password",
                    "Activo",
                    1,
                    101,
                    "img1.png"
                ),
                new User(
                    2,
                    "user2@example.com",
                    "Usuario",
                    "Dos",
                    "555-2222",
                    "password",
                    "Inactivo",
                    2,
                    102,
                    "img2.png"
                )
            };
            return Result<List<IUser>>.Success(users);
        }

        /// <summary>
        /// Logic to remove a user by its ID.
        /// </summary>
        /// <param name="userId">ID of the user to remove</param>
        /// <returns>Result indicating success or failure</returns>
        public Result<bool> RemoveUser(int userId)
        {
            // TODO: Add the logic to remove the user from the data source
            return Result<bool>.Success(true);
        }

        // Additional methods

        /// <summary>
        /// Logic to authenticate the user with email and password.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>Result indicating whether the authentication was successful</returns>
        public Result<bool> Authenticate(string email, string password)
        {
            // Simple example
            if (email == "user@example.com" && password == "password")
                return Result<bool>.Success(true);

            return Result<bool>.Failure("Invalid credentials");
        }

        /// <summary>
        /// Logic to assign a role to a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="roleId">Role ID</param>
        /// <returns>Result indicating success or failure</returns>
        public Result<bool> AssignRole(int userId, int roleId)
        {
            // Update the RoleId property only if the user matches
            if (UserId == userId)
            {
                RoleId = roleId;
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("User not found");
        }

        /// <summary>
        /// Logic to update the user's password.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newPassword">New password to set</param>
        /// <returns>Result indicating success or failure</returns>
        public Result<bool> UpdatePassword(int userId, string newPassword)
        {
            if (UserId == userId)
            {
                UserPassword = newPassword;
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("User not found");
        }
    }
}
