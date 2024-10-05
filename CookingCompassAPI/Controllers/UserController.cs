using CookingCompassAPI.Domain;
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

        [HttpGet("{id}")]

        public User GetById (int id) 
        {
            return _userService.GetById(id);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpPost]

        public User SaveUser (User user)
        {
            return _userService.SaveUser(user);
        }

        [HttpDelete("{id}")]
        public void DeleteUser (int id) 
        {
            _userService.RemoveUser(id);
        }
    }
}
