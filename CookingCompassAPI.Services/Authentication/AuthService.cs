using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Authentication.PasswordHash;
using CookingCompassAPI.Services.Authentication.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Authentication
{
    public class AuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHasher;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHashService passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public UserDTO Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be null or empty.");
            }

            var user = _userRepository.GetByEmail(email);

            if (user == null)
            {
                throw new ArgumentException("Invalid Username or Password");
            }

            var token = _tokenService.GenerateToken(user);

            return new UserDTO
            {
                Id = user.Id,

                Name = user.Name,

                Token = token,
            };
        }
    }

}


