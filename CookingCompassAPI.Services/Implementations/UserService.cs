using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
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

        public UserService (CookingCompassApiDBContext cookingApiDBContext, IUserRepository userRepository)
        {
            _cookingCompassApiDBContext = cookingApiDBContext;
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById (int id) 
        {
            return _userRepository.GetById(id);
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

        public User SaveUser (User user)
        {
            bool userExists = _userRepository.GetAny(user.Id);

            if (!userExists)
            {
              user = _userRepository.Add(user);
            }
            else
            {
                user = _userRepository.Update(user);
            }

            _cookingCompassApiDBContext.SaveChanges();

            return user;
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
