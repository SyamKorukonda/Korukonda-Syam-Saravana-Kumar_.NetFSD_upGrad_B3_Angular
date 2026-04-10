using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApplication16.Models;

namespace WebApplication16.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IConfiguration config, UserManager<ApplicationUser> userManager)  
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateJSONWebToken(ApplicationUser userObj)
        {
            var userRoles = await _userManager.GetRolesAsync(userObj);

            var key = _config["Jwt:Key"];   // it should be same as appsettings.json for settings
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            SigningCredentials credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256);

            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Convert.ToString(userObj.UserName)),
                new Claim(ClaimTypes.Name, userObj.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // (JWT ID) Claim
            };

            // Add roles to claims
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));


            // Get duration from appsettings.json
            double duration = Convert.ToDouble(_config["Jwt:DurationInMinutes"]);

            JwtSecurityToken token = new JwtSecurityToken(
                            issuer: "YourIssuer",
                            audience: "YourAudience",
                            claims: authClaims,
                            expires: DateTime.Now.AddMinutes(duration),
                            signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);    // Generate JWT Token string 

            return tokenString;
        }
    }
}
