using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Controllers
{
    [Route("CookingCompassAPI/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager) 
        {
            
            _userService = userService;
            _userManager = userManager;

        }

        [HttpGet("userId/{id}")]

        public UserDTO GetById (int id) 
        {
            return _userService.GetById(id);
        }

        [HttpGet("username/{username}")]

        public User GetUserWithRecipes (string username)
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
        public async Task<IActionResult> Login(LoginResponseDTO loginResponseDTO)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(loginResponseDTO);

                if (result.Succeeded)
                {
                    return Ok("Login successful!"); 
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
    }
}
