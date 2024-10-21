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
        private readonly DbSet<Ingredient> _ingredientDbSet;
        private readonly CookingCompassApiDBContext _dbContext;


        public RecipeRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbContext = cookingCompassApiDBContext;

            _dbSet = _dbContext.Set<Recipe>();
          
            _ingredientDbSet = _dbContext.Set<Ingredient>();
        }

        public List<Recipe> GetAll()
        {
           return _dbSet
             .Include(r => r.RecipeIngredients)
             .ThenInclude(ri => ri.Ingredient)
             .Include(r => r.Comments)
             .ThenInclude(c => c.User)
             .Include(r => r.User)
             .ToList();
        }

        public Recipe GetById(int id)
        {
            return _dbSet.FirstOrDefault(recipe => recipe.Id == id);
        }

        public bool GetAny(int id)
        {
            return _dbSet.Any(recipe => recipe.Id == id);
        }

        public Recipe Add (Recipe recipe)
        {
            _dbSet.Add(recipe);
            _dbContext.SaveChanges();
            return recipe;
        }

        public Recipe Update (Recipe recipe)
        {
            _dbSet.Update(recipe);
            _dbContext.SaveChanges();
            return recipe;
        }

        public void Remove(Recipe recipe)
        {
            _dbSet.Remove(recipe);
            _dbContext.SaveChanges();
        }
    }
}
