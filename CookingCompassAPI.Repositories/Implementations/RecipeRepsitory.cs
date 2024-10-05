using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class RecipeRepository : IRecipeRepository
    {

        private readonly DbSet<Recipe> _recipeDbSet;
        private readonly DbSet<Ingredient> _ingredientDbSet;
        private readonly CookingCompassApiDBContext _dbContext;


        public RecipeRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _recipeDbSet = _dbContext.Set<Recipe>();
            _recipeDbSet = _dbContext.Set<Recipe>();
            _ingredientDbSet = _dbContext.Set<Ingredient>();
        }

        public List<Recipe> GetAll()
        {
           return _recipeDbSet.ToList();
        }

        public Recipe GetById(int id)
        {
            return _recipeDbSet.FirstOrDefault(recipe => recipe.Id == id);
        }

        public bool GetAny(int id)
        {
            return _recipeDbSet.Any(recipe => recipe.Id == id);
        }

        //public list<recipe> getbycategory(string category)
        //{

        //    return _dbset.where(recipe => recipe.category.name == category).tolist();

        //}

        //public list<recipe> getbydifficulty(string difficultylevel) //it will work if the difficultylevel matches exactly the enum values
        //{

        //    return _dbset.where(recipe => recipe.difficulty.tostring() == difficultylevel).tolist();

        //}

        //public list<recipe> getbyingredient(list<string> ingredients)
        //{

        //    return _dbset.where(recipe => recipe.ingredients.any(ing => ingredients.contains(ing.name))).tolist();

        //}

        public Recipe AddRecipeWithIngredient(Recipe recipe, List<Ingredient> ingredients)
        {
            // Add the Recipe to the DbContext
            _recipeDbSet.Add(recipe);

            // Associate each Ingredient with the Recipe and add them
            foreach (var ingredient in ingredients)
            {
                ingredient.Recipe = recipe; // Link ingredient to the newly created recipe
                _ingredientDbSet.Add(ingredient);
            }

            // Save changes to the database
            _dbContext.SaveChanges();

            return recipe;
        }

        public void UpdateIngredientsForRecipe(int recipeId, List<Ingredient> updatedIngredients)
        {
            // Get the existing ingredients for the recipe
            var existingIngredients = _ingredientDbSet.Where(ing => ing.RecipeId == recipeId).ToList();

            // Remove any ingredients that are no longer in the updated list
            foreach (var existingIngredient in existingIngredients)
            {
                if (!updatedIngredients.Any(ing => ing.Id == existingIngredient.Id))
                {
                    _ingredientDbSet.Remove(existingIngredient);
                }
            }

            // Add or update the remaining ingredients
            foreach (var ingredient in updatedIngredients)
            {
                if (ingredient.Id == 0) // New ingredient
                {
                    ingredient.RecipeId = recipeId;
                    _ingredientDbSet.Add(ingredient);
                }
                else // Update existing ingredient
                {
                    var existingIngredient = existingIngredients.First(ing => ing.Id == ingredient.Id);
                    existingIngredient.Name = ingredient.Name;
                    existingIngredient.Quantity = ingredient.Quantity;
                    existingIngredient.Unit = ingredient.Unit;
                }
            }
        }

        public Recipe Update(Recipe recipe)
        {
            _recipeDbSet.Update(recipe);

            return recipe;
        }
        public void Remove(Recipe recipe)
        {
            _recipeDbSet.Remove(recipe);
        }
    }
}
