using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain.DTO_s
{
    public class UserRegistrationDTO
    {
        public string UserName { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
