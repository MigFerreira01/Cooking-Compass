using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain
{
    public class RecipeWithIngredientsDto
    {
        public Recipe Recipe { get; set; } 
        public List<Ingredient> Ingredients { get; set; } 
    }
}
