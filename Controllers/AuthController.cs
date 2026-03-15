using Microsoft.AspNetCore.Mvc;
using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Mounret.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);

            if (result is null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var profile = await _service.GetProfileAsync(userId);

            return Ok(profile);
        }
    }
}
