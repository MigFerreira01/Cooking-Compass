using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class RecipeCategoryRepository : IRecipeCategoryRepository
    {

        private readonly DbSet<RecipeCategory> _dbSet;
        private readonly CookingCompassApiDBContext _dbContext;

        public RecipeCategoryRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<RecipeCategory>();
            _dbContext = cookingCompassApiDBContext;
        }

        public List<RecipeCategory> GetAll()
        {
            return _dbSet.ToList(); // SELECT * FROM RecipeCategorys;
        }

        public RecipeCategory GetById(int id) 
        {
            return _dbSet.FirstOrDefault(recipeCategory => recipeCategory.Id == id);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(recipeCategory => recipeCategory.Id == id);
        }

        public RecipeCategory Add (RecipeCategory recipeCategory) 
        {
            
            _dbSet.Add(recipeCategory);
            _dbContext.SaveChanges();
            return recipeCategory;

        }

        public RecipeCategory Update (RecipeCategory recipeCategory) 
        {
            
            _dbSet.Update(recipeCategory);
            _dbContext.SaveChanges();
            return recipeCategory;

        }

        public void Remove (RecipeCategory recipeCategory)
        {
            _dbSet.Remove(recipeCategory);
            _dbContext.SaveChanges();
        }
    }
}
