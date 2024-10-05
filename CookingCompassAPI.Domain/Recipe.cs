using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CookingCompassAPI.Domain
{
    public class Recipe
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection <Ingredient> Ingredients { get; set; } 

        //public string Description { get; set; }

        //public User Author { get; set; }

        //public RecipeCategory Category { get; set; }

        //public DifficultyLevel Difficulty { get; set; }

        //public ApprovalStatus Status { get; set; }

        //public List<Comment> Comments { get; set; }

        //public double AverageRating { get; set; }

        //public int Duration { get; set; }

    }
}
