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
        var emailBody = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html xmlns=""http://www.w3.org/1999/xhtml"">
            <head>
                <meta name=""viewport"" content=""width=device-width"" />
                <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
                <title>Actionable emails e.g. reset password</title>
                <style>
                    /* -------------------------------------
                        GLOBAL
                        A very basic CSS reset
                    ------------------------------------- */
                    * {
                        margin: 0;
                        padding: 0;
                        font-family: ""Helvetica Neue"", ""Helvetica"", Helvetica, Arial, sans-serif;
                        box-sizing: border-box;
                        font-size: 14px;
                    }
            
                    img {
                        max-width: 100%;
                    }
            
                    body {
                        -webkit-font-smoothing: antialiased;
                        -webkit-text-size-adjust: none;
                        width: 100% !important;
                        height: 100%;
                        line-height: 1.6;
                    }
            
                    /* Let's make sure all tables have defaults */
                    table td {
                        vertical-align: top;
                    }
            
                    /* -------------------------------------
                        BODY & CONTAINER
                    ------------------------------------- */
                    body {
                        background-color: #f6f6f6;
                    }
            
                    .body-wrap {
                        background-color: #f6f6f6;
                        width: 100%;
                    }
            
                    .container {
                        display: block !important;
                        max-width: 600px !important;
                        margin: 0 auto !important;
                        /* makes it centered */
                        clear: both !important;
                    }
            
                    .content {
                        max-width: 600px;
                        margin: 0 auto;
                        display: block;
                        padding: 20px;
                    }
            
                    /* -------------------------------------
                        HEADER, FOOTER, MAIN
                    ------------------------------------- */
                    .main {
                        background: #fff;
                        border: 1px solid #e9e9e9;
                        border-radius: 3px;
                    }
            
                    .content-wrap {
                        padding: 20px;
                    }
            
                    .content-block {
                        padding: 0 0 20px;
                    }
            
                    .header {
                        width: 100%;
                        margin-bottom: 20px;
                    }
            
                    .footer {
                        width: 100%;
                        clear: both;
                        color: #999;
                        padding: 20px;
                    }
                    .footer a {
                        color: #999;
                    }
                    .footer p, .footer a, .footer unsubscribe, .footer td {
                        font-size: 12px;
                    }
            
                    /* -------------------------------------
                        TYPOGRAPHY
                    ------------------------------------- */
                    h1, h2, h3 {
                        font-family: ""Helvetica Neue"", Helvetica, Arial, ""Lucida Grande"", sans-serif;
                        color: #000;
                        margin: 40px 0 0;
                        line-height: 1.2;
                        font-weight: 400;
                    }
            
                    h1 {
                        font-size: 32px;
                        font-weight: 500;
                    }
            
                    h2 {
                        font-size: 24px;
                    }
            
                    h3 {
                        font-size: 18px;
                    }
            
                    h4 {
                        font-size: 14px;
                        font-weight: 600;
                    }
            
                    p, ul, ol {
                        margin-bottom: 10px;
                        font-weight: normal;
                    }
                    p li, ul li, ol li {
                        margin-left: 5px;
                        list-style-position: inside;
                    }
            
                    /* -------------------------------------
                        LINKS & BUTTONS
                    ------------------------------------- */
                    a {
                        color: #1ab394;
                        text-decoration: underline;
                    }
            
                    .btn-primary {
                        text-decoration: none;
                        color: #fffff0;
                        background-color: #1ab394;
                        border: solid #1ab394;
                        border-width: 5px 10px;
                        line-height: 2;
                        font-weight: bold;
                        text-align: center;
                        cursor: pointer;
                        display: inline-block;
                        border-radius: 5px;
                        text-transform: capitalize;
                    }
            
                    /* -------------------------------------
                        OTHER STYLES THAT MIGHT BE USEFUL
                    ------------------------------------- */
                    .last {
                        margin-bottom: 0;
                    }
            
                    .first {
                        margin-top: 0;
                    }
            
                    .aligncenter {
                        text-align: center;
                    }
            
                    .alignright {
                        text-align: right;
                    }
            
                    .alignleft {
                        text-align: left;
                    }
            
                    .clear {
                        clear: both;
                    }
            
                    /* -------------------------------------
                        ALERTS
                        Change the class depending on warning email, good email or bad email
                    ------------------------------------- */
                    .alert {
                        font-size: 16px;
                        color: #fff;
                        font-weight: 500;
                        padding: 20px;
                        text-align: center;
                        border-radius: 3px 3px 0 0;
                    }
                    .alert a {
                        color: #fff;
                        text-decoration: none;
                        font-weight: 500;
                        font-size: 16px;
                    }
                    .alert.alert-warning {
                        background: #f8ac59;
                    }
                    .alert.alert-bad {
                        background: #ed5565;
                    }
                    .alert.alert-good {
                        background: #1ab394;
                    }
            
                    /* -------------------------------------
                        INVOICE
                        Styles for the billing table
                    ------------------------------------- */
                    .invoice {
                        margin: 40px auto;
                        text-align: left;
                        width: 80%;
                    }
                    .invoice td {
                        padding: 5px 0;
                    }
                    .invoice .invoice-items {
                        width: 100%;
                    }
                    .invoice .invoice-items td {
                        border-top: #eee 1px solid;
                    }
                    .invoice .invoice-items .total td {
                        border-top: 2px solid #333;
                        border-bottom: 2px solid #333;
                        font-weight: 700;
                    }
            
                    /* -------------------------------------
                        RESPONSIVE AND MOBILE FRIENDLY STYLES
                    ------------------------------------- */
                    @media only screen and (max-width: 640px) {
                        h1, h2, h3, h4 {
                            font-weight: 600 !important;
                            margin: 20px 0 5px !important;
                        }
            
                        h1 {
                            font-size: 22px !important;
                        }
            
                        h2 {
                            font-size: 18px !important;
                        }
            
                        h3 {
                            font-size: 16px !important;
                        }
            
                        .container {
                            width: 100% !important;
                        }
            
                        .content, .content-wrap {
                            padding: 10px !important;
                        }
            
                        .invoice {
                            width: 100% !important;
                        }
                    }
                </style>
            </head>
            <body>
            
            <table class=""body-wrap"">
                <tr>
                    <td></td>
                    <td class=""container"" width=""600"">
                        <div class=""content"">
                            <div style="" background-color: #fdbab6c4;"">
                                <table class=""main"" style=""margin: 5px 5px 5px 5px;"">
                                    <tr>
                                        <td class=""content-wrap"">
                                            <table  cellpadding=""0"" cellspacing=""0"">
                                                <tr>
                                                    <td>
                                                        <div style=""display: grid;"">
                                                            <div style=""place-self: center;"">
                                                                <img src=""cid:logo"">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class=""content-block"">
                                                        <h3>Activate your Vivint account</h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class=""content-block"">
                                                        We just need to validate your email address to activate your Vivint account.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class=""content-block"">
                                                         Simply click the following button:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class=""content-block aligncenter"">
                                                        <a href="" class=""btn-primary"">Activate</a>
                                                    </td>
                                                </tr>
                                                </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class=""footer"">
                                <table width=""100%"">
                                    <tr>
                                        <td class=""aligncenter content-block""> 
                                            <strong>Copyright</strong> <a>Peregrina SPA</a> <small>&copy; 2022-2023</small>
                                        </td>
                                    </tr>
                                </table>
            
                    </td>
                    <td></td>
                </tr>
            </table>
            
            </body>
            </html>";
        await _emailService.SendVerificationEmail(
                            request.Email,
                            "Vivint - Activate your account",
                            emailBody);

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
            IdRole = rol.Id,
            IdCustomer = customer.Id
        };

        //Creting the new Customer credentials and deleting the peding register
        await _credentialService.CreateCredentialAsync(credential);
         await _pendingRegistrationService.DeleteByIdAsync(pending.Id);

        return Ok(true);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> login(UserDTO request)
    {
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
        
        await _tokenHelper?.SetRefreshToken(refreshToken, credential);

        return Ok(token);
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
