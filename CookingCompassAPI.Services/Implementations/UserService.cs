using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
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

        public UserService (CookingCompassApiDBContext cookingApiDBContext, IUserRepository userRepository, TranslateUser translateUser)
        {
            _cookingCompassApiDBContext = cookingApiDBContext;
            _userRepository = userRepository;
            _translateUser = translateUser;
        }

        public List<UserDTO> GetAll()
        {
            List<User> users = _userRepository.GetAll();

            List<UserDTO> userDTOs = users.Select(_translateUser.MapUserDTO).ToList();
            return userDTOs;
        }

        public UserDTO GetById (int id) 
        {
            var user = _userRepository.GetById (id);
            
            return _translateUser.MapUserDTO(user);
        }

        public User GetUserWithRecipes (string username) 
        {
            var user = _userRepository.GetByUsername (username);

            int id = user.Id;

            return _userRepository.GetUserWithRecipes (id);
        }

        public User GetByUsername (string username) 
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User '{username}' not found");
            }

            return user; 
        }

        public bool UserExists (string username)
        {
            return _userRepository.UserExists(username);
        }

        public void SaveUser (UserDTO userDTO)
        {
            bool userExists = _userRepository.GetAny(userDTO.Id);

            if (!userExists)
            {
              var user = _translateUser.MapUser(userDTO);

              user = _userRepository.Add(user);
            }
            else
            {
                var user = _translateUser.MapUser(userDTO);

                user = _userRepository.Update(user);
            }
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

    public class TranslateUser
    {
        private readonly CookingCompassApiDBContext _cookingApiDBContext;

        public TranslateUser(CookingCompassApiDBContext cookingApiDBContext)
        {
            _cookingApiDBContext = cookingApiDBContext;
        }

        public User MapUser (UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            var existingUser = _cookingApiDBContext.Users
                  .AsNoTracking() // Prevent tracking issues
                  .SingleOrDefault(u => u.Id == userDTO.Id);
             return existingUser;
        }

        public UserDTO MapUserDTO (User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new UserDTO
            {
                Id = user.Id,

                Name = user.Name,

                Email = user.Email,

                Password = user.Password,

                IsAdmin = user.IsAdmin,

                IsBlocked = user.IsBlocked,

                RegistrationDate = user.RegistrationDate
            };
        }
    }


}
