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
            _dbContext = cookingCompassApiDBContext;

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

        public void Remove(Recipe recipe)
        {
            _recipeDbSet.Remove(recipe);
            _dbContext.SaveChanges();
        }
    }
}
