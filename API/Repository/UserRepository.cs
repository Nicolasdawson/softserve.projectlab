using API.Data;
using API.Data.Models;
using API.Data.Models.DTOs.User;
using API.Repository.IRepository;
using API.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UserRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
        {
            this.db = db;
            this.mapper = mapper;
            this.configuration = configuration;
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

        public async Task<(bool IsLoginSucces, JWTTokenResponseDto TokenResponse)> LoginUser(UserLoginDto userLoginDto)
        {
            if(string.IsNullOrEmpty(userLoginDto.Email) || string.IsNullOrEmpty(userLoginDto.Password))
            {
                return (false, null);
            }

            var user = await db.User.Where(u => u.Email.ToLower() == userLoginDto.Email.ToLower()).FirstOrDefaultAsync();

            if(user == null)
            {
                return (false, null);
            }

            if(user.Password != PasswordHasher.HashPassword(userLoginDto.Password.Trim()))
            {
                return (false, null);
            }

            // Generate JWT token

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var calims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: calims,
                expires: DateTime.Now.AddDays(60),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var result = new JWTTokenResponseDto
            {
                AccessToken = jwt                
            };

            return (true, result);

         }

        //public async Task<User> LoginUser(UserLoginDto userLoginDto)
        //{
        //    var user = await db.User.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email.Trim());
        //    if (user == null || user.Password != PasswordHasher.HashPassword(userLoginDto.Password))
        //    {
        //        return null;
        //    }

        //    return user;
        //}

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
