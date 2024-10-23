using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CookingCompassAPI.Controllers
{
    [Route("CookingCompassAPI/[Controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, UserManager<User> userManager, IConfiguration configuration) 
        {
            
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet("userId/{id}")]

        public UserDTO GetById (int id) 
        {
            return _userService.GetById(id);
        }

        [HttpGet("username/{username}")]

        public UserDTO GetUserWithRecipes (string username)
        {
            return _userService.GetUserWithRecipes(username);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDTO userRegistrationDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(userRegistrationDTO);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginResponseDTO loginResponseDTO)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(loginResponseDTO);

                if (result.Succeeded)
                {
                    var user = await _userService.GetByNameAsync("AdminUser");

                    return Ok(GenerateJWTToken(user)); 
                }
                else if (result.IsLockedOut)
                {
                    return BadRequest("Your account is locked."); 
                }
                else
                {
                    return Unauthorized("Invalid login attempt."); 
                }
            }

            return BadRequest(ModelState); 

        }

        [HttpDelete("{id}")]
        public void DeleteUser (int id) 
        {
            _userService.RemoveUser(id);
        }

        private string GenerateJWTToken(UserDTO user)
        {
            var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
    };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration.GetSection("ApplicationSettings:JWT_Secret").Value)
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
