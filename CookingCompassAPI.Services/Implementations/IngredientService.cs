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
    public class IngredientService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IIngredientRepository _ingredientRepository;

        public IngredientService (CookingCompassApiDBContext cookingApiDBContext, IIngredientRepository ingredientRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _ingredientRepository = ingredientRepository;
        }

        public List<Ingredient> GetAll()
        {
            return _ingredientRepository.GetAll();
        }

        public Ingredient GetById (int id) 
        {
            return _ingredientRepository.GetById(id);
        }


        public Ingredient SaveIngredient (Ingredient ingredient)
        {
            bool ingredientExists = _ingredientRepository.GetAny(ingredient.Id);

            if (!ingredientExists)
            {
              ingredient = _ingredientRepository.Add(ingredient);
            }
            else
            {
                ingredient = _ingredientRepository.Update(ingredient);
            }

            return ingredient;
        }

        public void RemoveIngredient (int id)
        {
            Ingredient ingredientResult = _ingredientRepository.GetById(id);

            if (ingredientResult != null)
            {
                _ingredientRepository.Remove(ingredientResult);

                _cookingApiDBContext.SaveChanges();
            }
        }

    }




}
