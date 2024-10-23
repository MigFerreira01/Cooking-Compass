using CookingCompassAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }   

        public bool IsBlocked { get; set; }

        public List<RecipeDTO> Recipes { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
