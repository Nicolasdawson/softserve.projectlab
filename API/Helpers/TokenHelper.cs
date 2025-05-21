using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.DTO;
using API.Models;
using API.Services;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICredentialService _credentialService;
        public TokenHelper(IConfiguration configuration, IHttpContextAccessor contextAccessor, ICredentialService credentialService)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _credentialService = credentialService;
        }
        public void CreateToken(string email, out string jwt)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(                
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            jwt = new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null; // Invalid token 
            }
        }

        public RefreshTokenDTO GetRefreshToken()
        {
            var refreshToken = new RefreshTokenDTO
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public async Task SetRefreshToken(RefreshTokenDTO newRefreshToken, Credential credential)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };

            _contextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
                        
            credential.RefreshToken = newRefreshToken.Token;
            credential.TokenCreated = newRefreshToken.Created;
            credential.TokenExpires = newRefreshToken.Expires;

            // Updating credential in DB
            await _credentialService.UpdateCredentialAsync(credential);
        }
    }
}
