using API.Data;
using API.Data.Models;
using API.Data.Models.DTOs.User;
using API.Repository.IRepository;
using API.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<User> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await db.User.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email.Trim());
            if (user == null || user.Password != PasswordHasher.HashPassword(userLoginDto.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<bool> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var userExists = await db.User.FirstOrDefaultAsync(u => u.Email == userRegisterDto.Email.Trim());

            if (userExists != null)
            {
                return false;
            }

            userRegisterDto.Password = PasswordHasher.HashPassword(userRegisterDto.Password.Trim());

            var user = mapper.Map<User>(userRegisterDto);

            await db.User.AddAsync(user);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
