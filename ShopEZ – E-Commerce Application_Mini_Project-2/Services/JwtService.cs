using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication17.Models;

namespace WebApplication17.Services
{

    public interface IJwtService
    {
        string GenerateJSONWebToken(User userObj);
    }

    public class JwtService : IJwtService
    {
        // IConfiguration to read values from appsettings.json
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(User userObj)
        {
            // Fetch ALL settings from appsettings.json
            var key = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var durationInMinutes = Convert.ToDouble(_config["Jwt:DurationInMinutes"]);
            
            //Create security key using secret key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create claims (user information inside token)
            List<Claim> authClaims = new List<Claim>
            {
                // Unique user ID (used for authorization)
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userObj.UserId)),
                 // Username
                new Claim(ClaimTypes.Name, userObj.UserName),
                 // Role (Admin / Customer)
                new Claim(ClaimTypes.Role, userObj.Role),
               // Unique token ID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create JWT token
            JwtSecurityToken token = new JwtSecurityToken(
                            issuer: issuer,
                            audience: audience,
                            claims: authClaims,
                            expires: DateTime.Now.AddMinutes(durationInMinutes),
                            signingCredentials: credentials);

            //Convert token object to string
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString= tokenHandler.WriteToken(token);
            // Return token to client
            return tokenString;
        }
    }
}

