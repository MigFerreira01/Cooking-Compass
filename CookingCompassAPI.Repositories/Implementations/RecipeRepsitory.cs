using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class RecipeRepository : IRecipeRepository
    {

        private readonly DbSet<Recipe> _dbSet;


        public RecipeRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<Recipe>();
        }

        public List<Recipe> GetAll()
        {
           return _dbSet.ToList();
        }

        public Recipe GetById(int id)
        {
            return _dbSet.FirstOrDefault(recipe => recipe.Id == id);
        }

        public bool GetAny(int id)
        {
            return _dbSet.Any(recipe => recipe.Id == id);
        }

        //public List<Recipe> GetByCategory (string category) 
        //{

        //    return _dbSet.Where(recipe => recipe.Category.Name == category).ToList();

        //}

        //public List<Recipe> GetByDifficulty (string difficultyLevel) //It will work if the difficultyLevel matches exactly the enum values
        //{

        //    return _dbSet.Where(recipe => recipe.Difficulty.ToString() == difficultyLevel).ToList();

        //}

        //public List<Recipe> GetByIngredient (List<string> ingredients) 
        //{

        //    return _dbSet.Where(recipe => recipe.Ingredients.Any(ing => ingredients.Contains(ing.Name))).ToList();

        //}

        public Recipe Add(Recipe recipe)
        {

            _dbSet.Add(recipe);

            return recipe;

        }

        public Recipe Update(Recipe recipe)
        {

            _dbSet.Update(recipe);

            return recipe;

        }

        public void Remove(Recipe recipe)
        {
            _dbSet.Remove(recipe);
        }
    }
}
