using CookingCompassAPI.Domain.DTO_s;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace CookingCompassAPI.Domain
{
    public class User : IdentityUser<int>
    {

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set;}

        public DateTime RegistrationDate { get; set; }

        public List<Recipe> Recipes { get; set; }

    }
}
