using IceCreamStore.Shared.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IceCreamStore.API.Services
{
    public class TokenService(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration) =>
            new()
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = GetSecurityKey(configuration)
            };

        private static SymmetricSecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            return securityKey;
        }

        public string GeneratedToken(LoggedInUser loggedInUser)
        {
            var securityKey = GetSecurityKey(configuration);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["Jwt:Issuer"];
            var expireInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireInMinute"]);

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.id.ToString()),
                new Claim(ClaimTypes.Name, loggedInUser.Name),
                new Claim(ClaimTypes.Email, loggedInUser.Email),
                new Claim(ClaimTypes.StreetAddress, loggedInUser.Address),
            ];

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "*",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireInMinutes),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
