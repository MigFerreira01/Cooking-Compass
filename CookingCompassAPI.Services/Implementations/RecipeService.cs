using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.Extensions.Logging;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeService : IRecipeService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IRecipeRepository _recipeRepository;

        private readonly TranslateUser _translateUser;

        private readonly IUserService _userService;

        private readonly TranslateRecipe _translateRecipe;

        private readonly ILogger<RecipeService> _logger;

        public RecipeService(CookingCompassApiDBContext cookingApiDBContext, IRecipeRepository recipeRepository, ILogger<RecipeService> logger, TranslateRecipe translateRecipe, IUserService userService, TranslateUser translateUser)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _recipeRepository = recipeRepository;
            _translateRecipe = translateRecipe;
            _logger = logger;
            _userService = userService;
            _translateUser = translateUser;
        }

        public List<RecipeDTO> GetAll()
        {

            List<Recipe> recipes = _recipeRepository.GetAll();

            List<RecipeDTO> recipeDTOs = recipes.Select(_translateRecipe.MapRecipeDTO).ToList();

            return recipeDTOs;
        }

        public void AddRecipe(RecipeDTO recipeDTO)
        {
            var existingUser = _userService.GetByUsername(recipeDTO.User);

            if (existingUser == null)
            {
                throw new ArgumentException($"User '{recipeDTO.User}' does not exist.");
            }

            var recipe = _translateRecipe.MapRecipe(recipeDTO);

            recipe.User = existingUser;

            _recipeRepository.Add(recipe);
        }

        public void RemoveRecipe(int id)
        {
            Recipe recipeResult = _recipeRepository.GetById(id);

            if (recipeResult != null)
            {
                _recipeRepository.Remove(recipeResult);
            }
        }

    }

}
