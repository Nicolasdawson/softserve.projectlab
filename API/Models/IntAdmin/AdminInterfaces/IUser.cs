namespace API.Models.IntAdmin.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string UserEmail { get; set; }
        string UserFirstName { get; set; }
        string UserLastName { get; set; }
        string UserPhone { get; set; }
        string UserPassword { get; set; }
        string UserStatus { get; set; }
        string UserImage { get; set; }
        int RoleId { get; set; }
        int BranchId { get; set; }

        // CRUD methods
        Result<IUser> AddUser(IUser user);
        Result<IUser> UpdateUser(IUser user);
        Result<IUser> GetUserById(int userId);
        Result<List<IUser>> GetAllUsers();
        Result<bool> RemoveUser(int userId);

        // Additional methods
        Result<bool> Authenticate(string email, string password);
        Result<bool> AssignRole(int userId, int roleId);
        Result<bool> UpdatePassword(int userId, string newPassword);
    }
}
