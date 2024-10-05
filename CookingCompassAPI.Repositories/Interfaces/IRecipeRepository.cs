using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface IRecipeRepository
    {

        List<Recipe> GetAll();

        Recipe GetById(int id);

        Recipe AddRecipeWithIngredient(Recipe recipe, List<Ingredient> ingredients);

        bool GetAny(int id);

        //List<Recipe> GetByCategory (string category);

        //List<Recipe> GetByDifficulty(string difficultyLevel);

        //List<Recipe> GetByIngredient (List<string> ingredients);

        void UpdateIngredientsForRecipe(int recipeId, List<Ingredient> updatedIngredients);

        Recipe Update (Recipe recipe);

        void Remove(Recipe recipe);


    }
}