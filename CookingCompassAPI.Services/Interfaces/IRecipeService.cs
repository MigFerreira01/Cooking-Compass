using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IRecipeService
    {

        List<Recipe> GetAll();

        Recipe GetById (int id);

        //List<Recipe> GetByCategory (string category);

        //List<Recipe> GetByDifficulty (string difficulty);

        //List<Recipe> GetByIngredient(List<string> ingredietn);

        Recipe SaveRecipe (Recipe recipe, List<Ingredient> ingredients);

        void RemoveRecipe (int id);
    }
}
