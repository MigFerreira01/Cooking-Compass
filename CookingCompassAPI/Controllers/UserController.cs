using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookingCompassAPI.Controllers
{
    [Route("CookingCompassAPI/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            
            _userService = userService;

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
        public List<UserDTO> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpPost]

        public void SaveUser (UserDTO userDTO)
        {
            _userService.SaveUser(userDTO);
        }

        [HttpDelete("{id}")]
        public void DeleteUser (int id) 
        {
            _userService.RemoveUser(id);
        }
    }
}
