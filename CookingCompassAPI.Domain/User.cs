using CookingCompassAPI.Domain.DTO_s;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace CookingCompassAPI.Domain
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set;}

        public DateTime RegistrationDate { get; set; }

        public List<Recipe> Recipes { get; set; }

        //public ClaimsIdentity? Username { get; set; }
    }
}
