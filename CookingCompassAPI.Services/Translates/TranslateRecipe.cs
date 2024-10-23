using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Implementations;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Services.Translates
{
    public class TranslateRecipe
    {
        private IUserService _userService;

        private readonly TranslateUser _translateUser;

        private IIngredientService _ingredientService;

        private IIngredientRepository _ingredientRepository;

        private CookingCompassApiDBContext _dbContext;

        public TranslateRecipe(IUserService userService, TranslateUser translateUser, IIngredientService ingredientService, IIngredientRepository ingredientRepository, CookingCompassApiDBContext dBContext)
        {
            _userService = userService;
            _translateUser = translateUser;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
            _dbContext = dBContext;
        }

        public async Task<Recipe> MapRecipeAsync (RecipeDTO recipeDTO)
        {
            if (recipeDTO == null)
            {
                throw new ArgumentNullException(nameof(recipeDTO));
            }

            var user = await _userService.GetByNameAsync(recipeDTO.User);


            return new Recipe
            {
                Id = recipeDTO.Id,
                Name = recipeDTO.Name ?? "Unnamed Recipe",
                Description = recipeDTO.Description ?? "No description available.",
                Duration = recipeDTO.Duration,
                RecipeIngredients = MapRecipeIngredients(recipeDTO.Ingredients),
                User = user,
                Difficulty = GetDifficultyLevelByString(recipeDTO.Difficulty),
                Category = GetRecipeCategoryByString(recipeDTO.Category),
                Status = GetApprovalStatusByString(recipeDTO.Status),
                Comments = await MapCommentsAsync(recipeDTO.Comments)
            };
        }

        private List<RecipeIngredient> MapRecipeIngredients(IEnumerable<RecipeIngredientDTO> ingredientDTOs)
        {
            var ingredientNames = ingredientDTOs.Select(i => i.IngredientName).Distinct().ToList();
            var existingIngredients = _dbContext.Ingredients
                .Where(i => ingredientNames.Contains(i.Name))
                .ToDictionary(i => i.Name, i => i);

            var recipeIngredients = new List<RecipeIngredient>();

            foreach (var ingredientDTO in ingredientDTOs)
            {
                if (ingredientDTO == null)
                {
                    throw new ArgumentNullException(nameof(ingredientDTO), "Ingredient data cannot be null.");
                }

                if (!existingIngredients.TryGetValue(ingredientDTO.IngredientName, out var existingIngredient))
                {
                    existingIngredient = new Ingredient
                    {
                        Name = ingredientDTO.IngredientName
                    };

                    _dbContext.Ingredients.Add(existingIngredient);
                    _dbContext.SaveChanges();

                    existingIngredients[ingredientDTO.IngredientName] = existingIngredient;
                }

                recipeIngredients.Add(new RecipeIngredient
                {
                    IngredientId = existingIngredient.Id,
                    Quantity = ingredientDTO.Quantity,
                    Unit = ingredientDTO.Unit,
                });
            }

            return recipeIngredients;
        }

        private async Task<List<Comment>> MapCommentsAsync (IEnumerable<CommentDTO> commentDTOs)
        {

            var comments = new List<Comment>();

            if (commentDTOs == null) return comments;

            foreach (var comment in commentDTOs)
            {
                if (comment == null)
                {
                    throw new ArgumentNullException(nameof(comment), "Comment data cannot be null.");
                }

                var user = await _userService.GetByNameAsync(comment.User);

                if (user == null)
                {
                    throw new ArgumentException($"User '{comment.User}' does not exist.");
                }

                comments.Add(new Comment
                {
                    Id = comment.Id,
                    User = user,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                });
            }

            return comments;
        }

        public RecipeDTO MapRecipeDTO(Recipe recipe) => new RecipeDTO
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Duration = recipe.Duration,
            Ingredients = recipe.RecipeIngredients.Select(ing => new RecipeIngredientDTO
            {
                IngredientID = ing.Ingredient?.Id ?? 0,
                Quantity = ing.Quantity,
                Unit = ing.Unit,
            }).ToList() ?? new List<RecipeIngredientDTO>(),
            User = recipe.User?.Name ?? "Unknown",
            Category = recipe.Category.ToString() ?? "Unknown",
            Difficulty = recipe.Difficulty.ToString() ?? "Unknown",
            Status = recipe.Status.ToString() ?? "Unknown",
            Comments = recipe.Comments.Select(comment => new CommentDTO
            {
                Id = comment.Id,
                User = comment.User?.Name ?? "Anonymous",
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
            }).ToList() ?? new List<CommentDTO>(),
        };

        private TEnum ParseEnum<TEnum>(string value) where TEnum : struct
        {
            if (Enum.TryParse(value, out TEnum enumValue))
            {
                return enumValue;
            }

            throw new ArgumentException($"Invalid value '{value}' for enum {typeof(TEnum).Name}");
        }

        private DifficultyLevel GetDifficultyLevelByString(string difficulty) => ParseEnum<DifficultyLevel>(difficulty);
        private ApprovalStatus GetApprovalStatusByString(string status) => ParseEnum<ApprovalStatus>(status);
        private RecipeCategory GetRecipeCategoryByString(string category) => ParseEnum<RecipeCategory>(category);
    }




}
    

