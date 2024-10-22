using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CookingCompassAPI.Services.Authentication.PasswordHash
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        
    }
}
