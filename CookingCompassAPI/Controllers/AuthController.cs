using CookingCompassAPI.Services.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CookingCompassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase 
    {
        private readonly AuthService _authService;

        public AuthController (AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
         public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var loginResponse = _authService.Login(request.Email, request.Password);
                return Ok(loginResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
