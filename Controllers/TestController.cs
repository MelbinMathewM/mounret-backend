using Microsoft.AspNetCore.Mvc;

namespace Mounret.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mounret Backend was is Running 🚀");
        }
    }
}
