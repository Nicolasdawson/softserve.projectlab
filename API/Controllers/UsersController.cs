using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.DTO;
using API.Helpers;
using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    public static Customer user = new Customer();
    public static Credential credential = new Credential();
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UsersController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Customer>> Register(UserDTO request)
    {
        PasswordHashHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);        

        user.Email = request.Email;
        credential.PasswordHash = passwordHash;
        credential.PasswordSalt = passwordSalt;

        return Ok(user);          
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> login(UserDTO request)
    {
        if (user.Email != request.Email)
        {
            return BadRequest("User not found");
        }

        if(!PasswordHashHelper.VerifyPasswordHash(request.Password, credential.PasswordHash, credential.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = CreateToken(user);

        return Ok(token);
    }

    private string CreateToken(Customer user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
