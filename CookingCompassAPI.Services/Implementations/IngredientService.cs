using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
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

        public List<IngredientDTO> GetAll()
        {
            var ingredients = _ingredientRepository.GetAll();
            return ingredients.Select(TranslateIngredient.MapIngredient).ToList();
        }

        public IngredientDTO GetById (int id) 
        {
            var ingredient = _ingredientRepository.GetById(id);
            return ingredient != null ? TranslateIngredient.MapIngredient(ingredient) : null;
        }


        public IngredientDTO SaveIngredient (IngredientDTO ingredientDTO)
        {
            var ingredient = TranslateIngredient.MapIngredient(ingredientDTO);
            bool ingredientExists = _ingredientRepository.GetAny(ingredient.Id);

            if (!ingredientExists)
            {
              ingredient = _ingredientRepository.Add(ingredient);
            }
            else
            {
                var existingIngredient = _ingredientRepository.GetById
            }

            return TranslateIngredient.MapIngredient(ingredient);
        }

        public void RemoveIngredient (int id)
        {
            var ingredient = _ingredientRepository.GetById(id);

            if (ingredient != null)
            {
                _ingredientRepository.Remove(ingredient);

                _cookingApiDBContext.SaveChanges();
            }
        }

    }

    public static class TranslateIngredient
    {

        public static Ingredient MapIngredient (IngredientDTO ingredientDTO)
        {
            return new Ingredient
            {
                Id = ingredientDTO.IngredientId,
                Name = ingredientDTO.Name,
                RecipeIngredients = new List<RecipeIngredient>()
            };
        }

        public static IngredientDTO MapIngredient(Ingredient ingredient)
        {
            return new IngredientDTO
            {
                IngredientId = ingredient.Id,
                Name = ingredient.Name
            };
        }

    }




}
