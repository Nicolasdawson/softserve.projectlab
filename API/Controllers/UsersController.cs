using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.DTO;
using API.Enums;
using API.Helpers;
using API.implementations.Infrastructure.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.IO;
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
 
    private readonly IConfiguration _configuration;
    private readonly IPendingRegistrationService _pendingRegistrationService;
    private readonly ICredentialService _credentialService;
    private readonly RoleServices _roleServices;
    private readonly ICustomerService _customerService;
    private readonly TokenHelper _tokenHelper;
    private readonly EmailService _emailService;    

    public UsersController(
        IConfiguration configuration, 
        IPendingRegistrationService pendingRegistrationService,
        ICredentialService credentialService,
        RoleServices roleServices,
        ICustomerService customerService,
        EmailService emailService,         
        TokenHelper tokenHelper)
    {
        _configuration = configuration;
        _pendingRegistrationService = pendingRegistrationService;
        _credentialService = credentialService;
        _roleServices = roleServices;
        _customerService = customerService;
        _emailService = emailService;
        _tokenHelper = tokenHelper;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<Customer>> Register(UserDTO request)
    {

        //Verify if already exists a request with the email
        var existsInPending = await _pendingRegistrationService.ExistsByEmailAsync(request.Email);
        if (existsInPending)
            return BadRequest("We already done the sending confirmation.");
        
        //Creating the passwords hash and salt 
        PasswordHashHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //Creatign token
        _tokenHelper.CreateToken(request.Email, out string jwt);

        //Creating the pending register
        var pending = new PendingRegistration
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            VerificationToken = jwt,
            Expiration = DateTime.UtcNow.AddHours(24)
        };


        //Sending email activation account
        string urlFrontend = "localhost:50309";
        try
        {
            // Ruta al archivo HTML
            string filePath = "C:\\Users\\matias\\source\\repos\\proyecto-final-catalogo-ventas\\API\\verificationMessage.html";

            // Leer el archivo HTML
            string htmlContent = await System.IO.File.ReadAllTextAsync(filePath);

            var tokenLink = Url.Action("ConfirmEmail", "accounts", new
            {
                userid = pending.Id,
                token = jwt
            }, HttpContext.Request.Scheme, urlFrontend);
           
            // Reemplazar el marcador de posición {0} con la URL de activación en el contenido del archivo
            htmlContent = htmlContent.Replace("{0}", tokenLink);  // Reemplazar la URL

            // Enviar el correo electrónico con el cuerpo del HTML modificado
            var response = await _emailService.SendVerificationEmail(
                request.Email,
                "Vivint - Activate your account",
                htmlContent); // Enviar el HTML completo como cuerpo del correo
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error al leer el archivo: " + ex.Message);
            return BadRequest(ex.Message);
        }

        //Storing the pending account registration
        await _pendingRegistrationService.CreateAsync(pending);
        return Ok(new { message = "Verify your account" });          
    }

    [HttpGet("activate/{token}")]
    public async Task<ActionResult<bool>> ActivateAccount( string token )
    {
        // Verifiying token
        var principal = _tokenHelper.ValidateToken(token);
        if (principal == null)
            return BadRequest("Invalid token");

        //Verifiying email claim exists
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        if (email == null) 
            return BadRequest("Invalid token");

        // Finding the pending register in DB
        var pending = await _pendingRegistrationService.GetByEmailAsync(email);
        if (pending == null || pending.VerificationToken != token)
            return BadRequest("No pending registration found");
        
        
        // Verifiying token expiration
        if (pending.Expiration < DateTime.Now)
            return BadRequest("Token expired");

        //Tengo que implementar la verificación de si el usuario existía antes como guest, si es asi, le asigno esa IdCustomer al campo y sino se deja null
        //Verifiying if this Customer was a guest, If exists, add that Id to field IdCustomer in PendingRegistration

        var customer = new Customer
        {
            Email = pending.Email,
            FirstName = pending.FirstName,
            LastName = pending.LastName,
            PhoneNumber = pending.PhoneNumber,
            StartDate = DateTime.Now,            
            IsCurrent = true,
            IsGuest = false
        };

        customer = await _customerService.CreateCustomerAsync(customer);

        //Finding the ID for rol : Normal
        var rol = await _roleServices.GetRoleByNameAsync(UserType.Normal.ToString());

        //Creating the credential for current user
        var credential = new Credential
        {
            PasswordHash = pending.PasswordHash,
            PasswordSalt = pending.PasswordSalt,
            IdRole = rol!.Id,
            IdCustomer = customer.Id
        };

        //Creting the new Customer credentials and deleting the peding register
        await _credentialService.CreateCredentialAsync(credential);
         await _pendingRegistrationService.DeleteByIdAsync(pending.Id);

        return Ok(new { message = true });
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> login([FromBody] UserDTO request)
    {
        Console.WriteLine(request);

        var customer = await _customerService.GetByEmailAsync(request.Email);
        if (customer.Email != request.Email)
        {
            return BadRequest("User not found");
        }
        var credential = await _credentialService.GetByIdCustomerAsync(customer.Id);

        if(!PasswordHashHelper.VerifyPasswordHash(request.Password, credential.PasswordHash, credential.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        var role = await _roleServices.GetById(credential.IdRole);

        string token = CreateToken(customer, role);
        var refreshToken = _tokenHelper.GetRefreshToken();
        
        await _tokenHelper.SetRefreshToken(refreshToken, credential);

        return Ok(new { Token = token, Expiration = DateTime.Now.AddDays(1) });
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized("No refresh token provider");

        var credential = await _credentialService.GetByRefreshTokenAsync(refreshToken);

        if (credential == null)
            return Unauthorized("Invalid refresh token.");

        if (credential.TokenExpires < DateTime.UtcNow)
            return Unauthorized("Token expired.");

        var role = await _roleServices.GetById(credential.IdRole);
        var customer = await _customerService.GetByIdAsync(credential.IdCustomer);
        string token = CreateToken(customer, role);
        var refToken = _tokenHelper.GetRefreshToken();

        await _tokenHelper?.SetRefreshToken(refToken, credential);

        return Ok(token);
    }

    private string CreateToken(Customer user, Role role)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Role, role.Name.ToString())            
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
