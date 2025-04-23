using API.Data.Models;
using API.Data.Models.DTOs.User;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<(bool IsUserRegistered, string Message)> RegisterUser(UserRegisterDto userRegisterDto);

        Task<User> LoginUser(UserLoginDto userLoginDto);
    }
}
