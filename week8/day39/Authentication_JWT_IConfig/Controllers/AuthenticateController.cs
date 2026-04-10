using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;
using WebApplication14.Services;

namespace WebApplication14.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        public List<UserModel> usersList = null;
        private readonly JwtService _jwtService;


        public AuthenticateController(JwtService jwtService)
        {

            _jwtService = jwtService;

            usersList = new List<UserModel>()
            {
                new UserModel() { UserName = "Admin", Password = "Admin123", Role="Admin" },
                new UserModel() { UserName = "Scott", Password = "Scott123", Role="Default" }
            };
        }


        [HttpPost]
        public IActionResult Login(UserModel requestUser)
        {
            // 1. Verify the user credentials
            UserModel? userObj = usersList.FirstOrDefault(user => user.UserName ==
            requestUser.UserName && user.Password == requestUser.Password);

            if (userObj != null)
            {
                // 2. Generate JWT Token
                string tokenStr = _jwtService.GenerateJSONWebToken(userObj);
                return Ok(new { token = tokenStr });
            }
            else
            {
                return BadRequest("Invalid user id or password");
            }       
        }
    }
}
