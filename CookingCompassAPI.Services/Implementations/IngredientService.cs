using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class IngredientService : IIngredientService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IIngredientRepository _ingredientRepository;

        private readonly TranslateIngredient _translateIngredient;

        public IngredientService (CookingCompassApiDBContext cookingApiDBContext, IIngredientRepository ingredientRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _ingredientRepository = ingredientRepository;
        }

        public List<IngredientDTO> GetAll()
        {
            List<Ingredient> ingredients = _ingredientRepository.GetAll();

            List<IngredientDTO> ingredientDTOs = ingredients.Select(_translateIngredient.MapIngredientDTO).ToList();
            
            return ingredientDTOs;
        }


        public void SaveIngredient (IngredientDTO ingredientDTO)
        {
            var ingredient = _translateIngredient.MapIngredient(ingredientDTO);

            _ingredientRepository.Add(ingredient);
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

    public class TranslateIngredient
    {

        public Ingredient MapIngredient (IngredientDTO ingredientDTO)
        {
            return new Ingredient
            {
                Id = ingredientDTO.IngredientId,
                Name = ingredientDTO.Name,
                RecipeIngredients = new List<RecipeIngredient>()
            };
        }

        public IngredientDTO MapIngredientDTO (Ingredient ingredient)
        {
            return new IngredientDTO
            {
                IngredientId = ingredient.Id,
                Name = ingredient.Name
            };
        }

    }




}
