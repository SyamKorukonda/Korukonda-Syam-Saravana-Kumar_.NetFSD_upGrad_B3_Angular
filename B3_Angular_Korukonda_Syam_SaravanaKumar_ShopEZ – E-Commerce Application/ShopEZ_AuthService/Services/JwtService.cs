using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ShopEZ_AuthService.Models;

namespace ShopEZ_AuthService.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
    public class JwtService:IJwtService
    {
        // IConfiguration to read values from appsettings.json
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            // Fetch ALL settings from appsettings.json

            var key = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.");
            //  ?? Null Coalescing Operator  

            var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured.");
            var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured.");
            var duration = Convert.ToDouble(_config["Jwt:DurationInMinutes"] ?? "60");

            //Create security key using secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create claims (user information inside token)
            var claims = new List<Claim>
            {
                // Unique user ID (used for authorization)
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                // Username
                new Claim(ClaimTypes.Name,           user.UserName),
                //EmailAddress
                new Claim(ClaimTypes.Email,          user.EmailAddress),
                // Role (Admin / Customer)
                new Claim(ClaimTypes.Role,           user.Role),
                // Unique token ID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: credentials
            );

            //Convert token object to string
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);
            // Return token to client
            return tokenString;
        }
    }
}
