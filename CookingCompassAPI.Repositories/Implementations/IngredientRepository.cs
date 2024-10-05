using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class IngredientRepository : IIngredientRepository
    {

        private readonly DbSet<Ingredient> _dbSet;


        public IngredientRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<Ingredient>();
        }

        public List<Ingredient> GetAll()
        {
            return _dbSet.ToList(); // SELECT * FROM Ingredients;
        }

        public Ingredient GetById(int id) 
        {
            return _dbSet.FirstOrDefault(ingredient => ingredient.Id == id);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(ingredient => ingredient.Id == id);
        }

        public Ingredient Add (Ingredient ingredient) 
        {
            
            _dbSet.Add(ingredient);

            return ingredient;

        }

        public Ingredient Update (Ingredient ingredient) 
        {
            
            _dbSet.Update(ingredient);

            return ingredient;

        }

        public void Remove (Ingredient ingredient)
        {
            _dbSet.Remove(ingredient);
        }
    }
}
