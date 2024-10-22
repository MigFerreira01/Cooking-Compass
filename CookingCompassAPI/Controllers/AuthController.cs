using CookingCompassAPI.Services.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CookingCompassAPI.Controllers
{
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
                var userDTO = _authService.Login(request.Email, request.Password);
                return Ok(userDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
