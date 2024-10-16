using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeService :IRecipeService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IRecipeRepository _recipeRepository;

        public RecipeService (CookingCompassApiDBContext cookingApiDBContext, IRecipeRepository recipeRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _recipeRepository = recipeRepository;
        }

        public List<RecipeDTO> GetAll()
        {
            
            var recipies = _recipeRepository.GetAll();
            return recipies.Select();
        }


        public void RemoveRecipe (int id)
        {
            Recipe recipeResult = _recipeRepository.GetById(id);

            if (recipeResult != null)
            {
                _recipeRepository.Remove(recipeResult);
            }
        }

    }

    public class TranslateRecipe
    {
        private IUserService _userService;
        private ICategoryService _categoryService;

        public static Recipe Recipe MapRecipe (RecipeDTO recipeDTO)
        {
            return new Recipe
            {
                Id = recipeDTO.Id,
                Name = recipeDTO.Name,
                Description = recipeDTO.Description,
                Category = recipeDTO.Category,
                Duration = recipeDTO.Duration,
                RecipeIngredients = recipeDTO.Ingredients.Select( i => new RecipeIngredient
                {
                    Ingredient = TranslateIngredient.MapIngredient(i),

                }).ToList(),
                Author = _userService.GetByUsername(recipeDTO.Author),
                Difficulty = GetDifficultyLevelByString(recipeDTO.Difficulty),
                Category = _categoryService.GetByName(recipeDTO.Category),
                Status = GetApprovalStatusByString(recipeDTO.Status),
                Comments = recipeDTO.Comments.Select(comment => new Comment
                {
                    Id = comment.Id,
                    Author = _userService.GetByUsername(comment.Author),
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,

                }).ToList(),

            };
        }

        private DifficultyLevel GetDifficultyLevelByString (string difficulty)
        {
            if (Enum.TryParse<DifficultyLevel>(difficulty, out var difficultyLevel)) 
            {
                return difficultyLevel;
            }
            throw new ArgumentException($"Invalid difficulty level '{difficulty}'");
        }

        private ApprovalStatus GetApprovalStatusByString(string status)
        {

            if (Enum.TryParse<ApprovalStatus>(status, out var approvalStatus))
            {
                return approvalStatus;
            }
            throw new ArgumentException($"Invalid approval status '{status}'");
        }
    }




}
