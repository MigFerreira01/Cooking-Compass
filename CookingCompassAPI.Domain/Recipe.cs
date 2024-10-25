﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CookingCompassAPI.Domain
{
    public class Recipe
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public List <RecipeIngredient> RecipeIngredients { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public RecipeCategory Category { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public ApprovalStatus Status { get; set; }

        public int Duration { get; set; }

        public string ImageUrl { get; set; }
    }
}
