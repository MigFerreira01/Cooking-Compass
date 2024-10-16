using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class CategoryService :  ICategoryService
    {
        private CookingCompassApiDBContext _cookingCompassApiDBContext;

        private ICategoryRepository _categoryRepository;

        public CategoryService(CookingCompassApiDBContext cookingCompassApiDBContext, ICategoryRepository categoryRepository)
        {
            _cookingCompassApiDBContext = cookingCompassApiDBContext;
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetByName (string name)
        {
            var category = _categoryRepository.GetByName(name);
            if (category == null)
            {
                throw new ArgumentException($"Category '{category}' not found");
            }

            return category;
        }

        public Category GetById (int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category UpdateCategory (Category category)
        {
            return _categoryRepository.Update(category);
        }

        public void RemoveCategory (int id)
        {
            Category categoryResult = _categoryRepository.GetById(id);

            if (categoryResult != null)
            {
                _categoryRepository.Remove(categoryResult);
            }
        }
    }
}
