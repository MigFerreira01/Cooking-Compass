using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class RatingRepository : IRatingRepository
    {

        private readonly DbSet<Rating> _dbSet;
        private readonly CookingCompassApiDBContext _dbContext;


        public RatingRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<Rating>();
            _dbContext = cookingCompassApiDBContext;
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
            _dbContext.SaveChanges();
            return rating;

        }

        public Rating Update (Rating rating) 
        {
            
            _dbSet.Update(rating);
            _dbContext.SaveChanges();
            return rating;

        }

        public void Remove (Rating rating)
        {
            _dbSet.Remove(rating);
            _dbContext.SaveChanges();
        }
    }
}
