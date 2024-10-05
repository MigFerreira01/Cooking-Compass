using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeService :IRecipeService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IRecipeRepository _recipeRepository;

        public RecipeService (CookingCompassApiDBContext cookingApiDBContext, IRecipeRepository recipeRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _recipeRepository = recipeRepository;
        }

        public List<Recipe> GetAll()
        {
            return _recipeRepository.GetAll();
        }

        public Recipe GetById (int id) 
        {
            return _recipeRepository.GetById(id);
        }

        //public List<Recipe> GetByCategory(string category)
        //{
        //    return _recipeRepository.GetByCategory(category);
        //}

        //public List<Recipe> GetByDifficulty(string difficultyLevel)
        //{
        //    return _recipeRepository.GetByDifficulty(difficultyLevel);
        //}

        //public List<Recipe> GetByIngredient(List<string> ingredients)
        //{
        //    return _recipeRepository.GetByIngredient(ingredients);
        //}

        public Recipe SaveRecipe (Recipe recipe, List<Ingredient> ingredients)
        {
            bool recipeExists = _recipeRepository.GetAny(recipe.Id);

            if (!recipeExists)
            {
                recipe = _recipeRepository.AddRecipeWithIngredient(recipe, ingredients);
            }
            else
            {
                recipe = _recipeRepository.Update(recipe);


                _recipeRepository.UpdateIngredientsForRecipe(recipe.Id, ingredients);
            }

            _cookingApiDBContext.SaveChanges();

            return recipe;
        }

        public void RemoveRecipe (int id)
        {
            Recipe recipeResult = _recipeRepository.GetById(id);

            if (recipeResult != null)
            {
                _recipeRepository.Remove(recipeResult);

                _cookingApiDBContext.SaveChanges();
            }
        }

    }




}
