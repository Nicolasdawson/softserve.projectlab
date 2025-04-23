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

        private User FromUserRegistrationModelToUserModel(UserRegisterDto userRegisterDto)
        {
            return new User
            {
                Name = userRegisterDto.Name,
                Email = userRegisterDto.Email,
                Password = PasswordHasher.HashPassword(userRegisterDto.Password.Trim())
            };
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

        public async Task<(bool IsUserRegistered, string Message)> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var userExists = await db.User.AnyAsync(u => u.Email.ToLower().Trim() == userRegisterDto.Email.ToLower().Trim());

            if(userExists)
            {
                return (false, "Email Address Already Registred");
            }

            var newUser = FromUserRegistrationModelToUserModel(userRegisterDto);

            db.User.Add(newUser);
            await db.SaveChangesAsync();
            return (true, "User Registered Successfully");
            //if (userExists != null)
            //{
            //    return false;
            //}

            //userRegisterDto.Password = PasswordHasher.HashPassword(userRegisterDto.Password.Trim());

            //var user = mapper.Map<User>(userRegisterDto);

            //await db.User.AddAsync(user);
            //await db.SaveChangesAsync();
            //return true;
        }
    }
}
