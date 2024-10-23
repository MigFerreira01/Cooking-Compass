using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeService : IRecipeService
    {

        private CookingCompassApiDBContext _dbContext;

        private IRecipeRepository _recipeRepository;

        private readonly TranslateUser _translateUser;

        private readonly IUserService _userService;

        private readonly TranslateRecipe _translateRecipe;

        private readonly ILogger<RecipeService> _logger;

        public RecipeService(CookingCompassApiDBContext cookingApiDBContext, IRecipeRepository recipeRepository, ILogger<RecipeService> logger, TranslateRecipe translateRecipe, IUserService userService, TranslateUser translateUser)
        {
            _dbContext = cookingApiDBContext;
            _recipeRepository = recipeRepository;
            _translateRecipe = translateRecipe;
            _logger = logger;
            _userService = userService;
            _translateUser = translateUser;
        }

        public async Task<RecipeDTO> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);

            if (recipe == null)
            {
                return null;
            }
            return _translateRecipe.MapRecipeDTO(recipe);
        }

        public async Task<Recipe> AddRecipeAsync (RecipeDTO recipeDTO)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existingUser = await _userService.GetByNameAsync(recipeDTO.User);
                if (existingUser == null)
                {
                    throw new ArgumentException($"User '{recipeDTO.User}' does not exist.");
                }

                var recipe = await _translateRecipe.MapRecipeAsync(recipeDTO);
                recipe.User = existingUser;

                _recipeRepository.Add(recipe);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return recipe;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> RemoveRecipeAsync(int id)
        {
            
            var recipe = await _recipeRepository.GetByIdAsync(id);

            if (recipe == null)
            {
                
                return false;
            }

            try
            {
                _recipeRepository.Remove(recipe);
                await _dbContext.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while removing the recipe.", ex);
            }
        }

    }

}
