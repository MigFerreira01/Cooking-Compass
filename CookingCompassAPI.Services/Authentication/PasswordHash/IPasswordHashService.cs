using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Authentication.PasswordHash
{
    public interface IPasswordHashService
    {
        string Hash(string password);

        bool Verify (string password, string hashedPassword);
    }
}
