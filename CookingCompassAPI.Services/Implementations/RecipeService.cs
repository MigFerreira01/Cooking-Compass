using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Domain;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeService : IRecipeService
    {

        private CookingCompassApiDBContext _dbContext;

        private IRecipeRepository _recipeRepository;

        private readonly TranslateUser _translateUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TranslateComment _translateComment;
        private readonly ICommentRepository _commentRepository;
        private readonly TranslateRecipe _translateRecipe;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RecipeService> _logger;

        public RecipeService(CookingCompassApiDBContext cookingApiDBContext, IRecipeRepository recipeRepository, ILogger<RecipeService> logger, TranslateRecipe translateRecipe, IUserRepository userRepository, TranslateUser translateUser, IHttpContextAccessor httpContextAccessor, TranslateComment translateComment, ICommentRepository commentRepository)
        {
            _dbContext = cookingApiDBContext;
            _recipeRepository = recipeRepository;
            _translateRecipe = translateRecipe;
            _userRepository = userRepository;
            _logger = logger;
            
            _translateUser = translateUser;
            _httpContextAccessor = httpContextAccessor;
            _translateComment = translateComment;
            _commentRepository = commentRepository;
        }

        public List<RecipeDTO> GetAllRecipes()
        {
            List<Recipe> recipes = _recipeRepository.GetAll();

            List<RecipeDTO> recipeDTOs = recipes.Select(_translateRecipe.MapRecipeDTO).ToList();

            return recipeDTOs;
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

        public async Task<RecipeDTO> AddRecipeAsync (RecipeDTO recipeDTO)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;

                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentException("User is not authenticated.");
                }

                var existingUser = _userRepository.GetByUsername(username);
                if (existingUser == null)
                {
                    throw new ArgumentException($"User '{username}' does not exist.");
                }

                var recipe = _translateRecipe.MapRecipe(recipeDTO);
                recipe.User = existingUser;
                

                _recipeRepository.Add(recipe);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                recipeDTO = _translateRecipe.MapRecipeDTO(recipe);

                return recipeDTO;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddCommentsToRecipeAsync(int recipeId, List<CommentDTO> commentDTOs)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;

                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentException("User is not authenticated.");
                }

                var existingUser = _userRepository.GetByUsername(username);
                if (existingUser == null)
                {
                    throw new ArgumentException($"User '{username}' does not exist.");
                }

                var recipe = await _recipeRepository.GetByIdAsync(recipeId);
                if (recipe == null)
                {
                    throw new ArgumentException($"Recipe with ID {recipeId} does not exist.");
                }

                var comments = commentDTOs.Select(commentDTO => _translateComment.MapComment(commentDTO)).ToList();

                foreach (var comment in comments)
                {
                    comment.User = existingUser;
                    comment.RecipeId = recipeId;
                    _commentRepository.Add(comment);
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
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

        public async Task<RecipeDTO> UpdateRecipeAsync (int recipeId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                Recipe recipe = await _recipeRepository.GetByIdAsync(recipeId);

                _recipeRepository.Update(recipe);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

               RecipeDTO recipeDTO = _translateRecipe.MapRecipeDTO(recipe);

                return recipeDTO;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }

}
