using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeCategoryService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IRecipeCategoryRepository _recipeCategoryRepository;

        public RecipeCategoryService (CookingCompassApiDBContext cookingApiDBContext, IRecipeCategoryRepository recipeCategoryRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _recipeCategoryRepository = recipeCategoryRepository;
        }

        public List<RecipeCategory> GetAll()
        {
            return _recipeCategoryRepository.GetAll();
        }

        public RecipeCategory GetById (int id) 
        {
            return _recipeCategoryRepository.GetById(id);
        }


        public RecipeCategory SaveRecipeCategory (RecipeCategory RecipeCategory)
        {
            bool RecipeCategoryExists = _recipeCategoryRepository.GetAny(RecipeCategory.Id);

            if (!RecipeCategoryExists)
            {
              RecipeCategory = _recipeCategoryRepository.Add(RecipeCategory);
            }
            else
            {
                RecipeCategory = _recipeCategoryRepository.Update(RecipeCategory);
            }

            return RecipeCategory;
        }

        public void RemoveRecipeCategory (int id)
        {
            RecipeCategory RecipeCategoryResult = _recipeCategoryRepository.GetById(id);

            if (RecipeCategoryResult != null)
            {
                _recipeCategoryRepository.Remove(RecipeCategoryResult);

                _cookingApiDBContext.SaveChanges();
            }
        }

    }




}
