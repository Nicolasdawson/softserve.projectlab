using API.Data.Models;
using API.Data.Models.DTOs.User;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(UserRegisterDto userRegisterDto);

        Task<User> LoginUser(UserLoginDto userLoginDto);
    }
}
