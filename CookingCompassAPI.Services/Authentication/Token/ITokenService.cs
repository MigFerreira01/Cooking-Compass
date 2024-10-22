using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Authentication.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
