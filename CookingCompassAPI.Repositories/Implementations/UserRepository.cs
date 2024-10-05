using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly DbSet<User> _dbSet;


        public UserRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<User>();
        }

        public List<User> GetAll()
        {
            return _dbSet.ToList(); 
        }

        public User GetById(int id) 
        {
            return _dbSet.FirstOrDefault(user => user.Id == id);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(user => user.Id == id);
        }

        public User Add (User user) 
        {
            
            _dbSet.Add(user);

            return user;

        }

        public User Update (User user) 
        {
            
            _dbSet.Update(user);

            return user;

        }

        public void Remove (User user)
        {
            _dbSet.Remove(user);
        }
    }
}
