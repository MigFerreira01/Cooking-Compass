using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly DbSet<User> _dbSet;
        private readonly CookingCompassApiDBContext _dbContext;


        public UserRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<User>();
            _dbContext = cookingCompassApiDBContext;
        }

        public List<User> GetAll()
        {
            return _dbSet.ToList(); 
        }

        public User GetById(int id) 
        {
            return _dbSet.FirstOrDefault(user => user.Id == id);
        }

        public User GetUserWithRecipes(int id)
        {
            using (var dbContext = _dbContext) 
            {
                var user = dbContext.Users
                    .Include(user => user.Recipes)
                    .FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                return user; 
            }
        }

        public bool UserExists(string username) 
        {
            return _dbSet.Any(user => user.Name.Equals(username));
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Users.SingleOrDefault(user => user.Name == username);
        }

        public User GetByEmail (string email)
        {
            return _dbContext.Users.SingleOrDefault(user => user.Email == email);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(user => user.Id == id);
        }

        public User Add (User user) 
        {
            
            _dbSet.Add(user);

            _dbContext.SaveChanges();

            return user;

        }

        public User Update (User user) 
        {
            
            _dbSet.Update(user);

            _dbContext.SaveChanges();

            return user;

        }

        public void Remove (User user)
        {
            _dbSet.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
