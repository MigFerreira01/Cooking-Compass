using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class RatingRepository : IRatingRepository
    {

        private readonly DbSet<Rating> _dbSet;


        public RatingRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<Rating>();
        }

        public List<Rating> GetAll()
        {
            return _dbSet.ToList(); // SELECT * FROM Ratings;
        }

        public Rating GetById(int id) 
        {
            return _dbSet.FirstOrDefault(rating => rating.Id == id);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(rating => rating.Id == id);
        }

        public Rating Add (Rating rating) 
        {
            
            _dbSet.Add(rating);

            return rating;

        }

        public Rating Update (Rating rating) 
        {
            
            _dbSet.Update(rating);

            return rating;

        }

        public void Remove (Rating rating)
        {
            _dbSet.Remove(rating);
        }
    }
}
