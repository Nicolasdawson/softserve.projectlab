using API.Data.Models;
using API.Data.Models.DTOs.User;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UsersController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        // api/users/login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await userRepository.LoginUser(userLoginDto);

            if (result.IsLoginSucces)
            {
                return Ok(result.TokenResponse);  
            }

            ModelState.AddModelError("LoginError", "Invalid Credentials");
            return base.BadRequest(ModelState);            
        }


        //[HttpPost("[action]")]
        //public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        //{
        //    var loggedInUser = await userRepository.LoginUser(userLoginDto);

        //    if (loggedInUser == null)
        //    {
        //        return NotFound(new { message = "El correo electrónico o la contraseña son incorrectos" });
        //    }

        //    // Generate JWT token

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var calims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id.ToString()),
        //        new Claim(ClaimTypes.Email, userLoginDto.Email),
        //        new Claim(ClaimTypes.Role, loggedInUser.Role),
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: configuration["JWT:Issuer"],
        //        audience: configuration["JWT:Audience"],
        //        claims: calims,
        //        expires: DateTime.Now.AddDays(60),
        //        signingCredentials: credentials
        //    );

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return Ok(new { acces_token = jwt, token_type = "bearer", user_id = loggedInUser.Id, user_name = loggedInUser.Name });
        //}


        // api/users/Register
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = await userRepository.RegisterUser(userRegisterDto);
            if(result.IsUserRegistered)
            {
                return Ok(result.Message);
            }

            ModelState.AddModelError("Email", result.Message);
            return BadRequest(ModelState);

            //var registrarionResult = await userRepository.RegisterUser(userRegisterDto);

            //if(registrarionResult)
            //{
            //    return StatusCode(StatusCodes.Status201Created);      
            //}

            //return BadRequest(new { message = "El correo electrónico ya está registrado" });
        }

    }
}
