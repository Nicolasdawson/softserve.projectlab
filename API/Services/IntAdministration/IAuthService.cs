using softserve.projectlabs.Shared.DTOs.User;
using softserve.projectlabs.Shared.Utilities;
using API.Models.IntAdmin;
using System.Threading.Tasks;

namespace API.Services.IntAdmin;

public interface IAuthService
{
    Task<Result<User>> RegisterAsync(UserCreateDto dto);
    Task<Result<string>> LoginAsync(UserLoginDto dto);
    Task<Result<string>> RefreshTokenAsync(string refreshToken);
}
