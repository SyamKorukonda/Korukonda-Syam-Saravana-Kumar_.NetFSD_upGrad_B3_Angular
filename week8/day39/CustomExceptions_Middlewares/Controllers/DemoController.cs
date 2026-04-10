using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            int x = 10, z = 0;
            z = x / id;
            return Ok("Result from the Demo controller. Result : " + z);
        }
    }
}
