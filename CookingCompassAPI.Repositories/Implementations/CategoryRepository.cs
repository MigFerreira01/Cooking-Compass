using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class CategoryRepository
    {
        private readonly DbSet <Category> _dbSet;
        private readonly CookingCompassApiDBContext _dbContext;

        public CategoryRepository(CookingCompassApiDBContext cookingCompassApiDBContext) 
        {
            _dbSet = cookingCompassApiDBContext.Set<Category>();
            _dbContext = cookingCompassApiDBContext;
        }

        public List<Category> GetAll()
        {
            return _dbSet.ToList();
        }

        public Category GetById (int id) 
        {
            return _dbSet.FirstOrDefault(category  => category.Id == id);
        }

        public Category GetByName (string name)
        {
            return _dbSet.FirstOrDefault(category =>category.Name == name);
        }

        public Category Add (Category category)
        {
            _dbSet.Add(category);
            _dbContext.SaveChanges();
            return category;

        }

        public Category Update (Category category)
        {
            _dbSet.Update(category);
            _dbContext.SaveChanges();
            return category;
        }

        public void Remove (Category category)
        {
            _dbSet.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}
