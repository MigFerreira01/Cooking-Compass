using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Services.Translates
{
    public class TranslateUser
    {
        private readonly CookingCompassApiDBContext _cookingApiDBContext;

        public TranslateUser(CookingCompassApiDBContext cookingApiDBContext)
        {
            _cookingApiDBContext = cookingApiDBContext;
        }

        public User MapUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            
            return new User
            {
                Id = userDTO.Id, 
                Name = userDTO.Name,
                Email = userDTO.Email,
                IsAdmin = userDTO.IsAdmin,
                IsBlocked = userDTO.IsBlocked,
                RegistrationDate = userDTO.RegistrationDate 
            };
        }

        public UserDTO MapUserDTO(User user)
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

                IsAdmin = user.IsAdmin,

                IsBlocked = user.IsBlocked,

                RegistrationDate = user.RegistrationDate
            };
        }
    }
}

