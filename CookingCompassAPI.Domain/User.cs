﻿using CookingCompassAPI.Domain.DTO_s;

namespace CookingCompassAPI.Domain
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set;}

        public DateTime RegistrationDate { get; set; }

        public List<Recipe> Recipes { get; set; }
    }
}
