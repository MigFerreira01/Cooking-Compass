using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
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
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "UserDTO cannot be null");
            }

            User user = _translateUser.MapUser(userDTO);
            if (user == null)
            {
                throw new InvalidOperationException("Mapped user cannot be null");
            }

            bool userExists = _userRepository.GetAny(userDTO.Id);
            if (!userExists)
            {
                _userRepository.Add(user);
            }
            else
            {
                _userRepository.Update(user);
            }

            _cookingCompassApiDBContext.SaveChanges();
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
