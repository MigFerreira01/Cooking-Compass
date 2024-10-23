using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class UserService :IUserService
    {

        private CookingCompassApiDBContext _cookingCompassApiDBContext;

        private IUserRepository _userRepository;

        private readonly TranslateUser _translateUser;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly TranslateRecipe _translateRecipe;

        public UserService (CookingCompassApiDBContext cookingApiDBContext, IUserRepository userRepository, TranslateUser translateUser, UserManager<User> userManager, SignInManager<User> signInManager, TranslateRecipe translateRecipe)
        {
            _cookingCompassApiDBContext = cookingApiDBContext;
            _userRepository = userRepository;
            _translateUser = translateUser;
            _userManager = userManager;
            _signInManager = signInManager;
            _translateRecipe = translateRecipe;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                IsAdmin = user.IsAdmin,
                IsBlocked = user.IsBlocked,
                RegistrationDate = user.RegistrationDate
            });
        }

        public UserDTO GetById (int id) 
        {
            var user = _userRepository.GetById (id);
            
            return _translateUser.MapUserDTO(user);
        }

        public UserDTO GetUserWithRecipes (string username) 
        {
            var user = _userRepository.GetByUsername (username);

            int id = user.Id;

            var userRecipes = _userRepository.GetUserWithRecipes (id);

            var userRecipesDTO = _translateUser.MapUserDTO(userRecipes);

            userRecipesDTO.Recipes = userRecipes.Recipes?.Select(_translateRecipe.MapRecipeDTO).ToList();

            return userRecipesDTO;
        }

        public async Task<UserDTO> GetByNameAsync (string username) 
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new ArgumentException($"User '{username}' not found");
            }

            var userDTO = _translateUser.MapUserDTO(user);

            return userDTO; 
        }

        public bool UserExists (string username)
        {
            return _userRepository.UserExists(username);
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var user = new User
            {
                UserName = userRegistrationDTO.UserName,
                Name = userRegistrationDTO.Name,
                Email = userRegistrationDTO.Email,
                IsAdmin = false,
                IsBlocked = false,
                RegistrationDate = DateTime.Now,
                Recipes = new List<Recipe>()
            };

            var result = await _userManager.CreateAsync(user, userRegistrationDTO.Password);

            return result;
        }

        public async Task<SignInResult> LoginUserAsync(LoginResponseDTO loginResponseDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginResponseDTO.Email);


            if (user == null)
            {
                return SignInResult.Failed;
            }

            if (user.IsBlocked)
            {
                return SignInResult.LockedOut;
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginResponseDTO.Password, false, lockoutOnFailure: true);

            return result;
        }

        public void RemoveUser (int id)
        {
            User userResult = _userRepository.GetById(id);

            if (userResult != null)
            {
                _userRepository.Remove(userResult);

                _cookingCompassApiDBContext.SaveChanges();
            }
        }

    }

}
