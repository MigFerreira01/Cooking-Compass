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

        public Recipe MapRecipe(RecipeDTO recipeDTO)
        {
            if (recipeDTO == null)
            {
                throw new ArgumentNullException(nameof(recipeDTO));
            }

            return new Recipe
            {
                Id = recipeDTO.Id,
                Name = recipeDTO.Name ?? "Unnamed Recipe",
                Description = recipeDTO.Description ?? "No description available.",
                Duration = recipeDTO.Duration,
                RecipeIngredients = MapRecipeIngredients(recipeDTO.Ingredients),
                User = _userService.GetByUsername(recipeDTO.User),
                Difficulty = GetDifficultyLevelByString(recipeDTO.Difficulty),
                Category = GetRecipeCategoryByString(recipeDTO.Category),
                Status = GetApprovalStatusByString(recipeDTO.Status),
                Comments = MapComments(recipeDTO.Comments)
            };
        }

        private List<RecipeIngredient> MapRecipeIngredients(IEnumerable<RecipeIngredientDTO> ingredientDTOs)
        {
            var recipeIngredients = new List<RecipeIngredient>();

            foreach (var ingredientDTO in ingredientDTOs)
            {
                if (ingredientDTO == null)
                {
                    throw new ArgumentNullException(nameof(ingredientDTO), "Ingredient data cannot be null.");
                }

                var existingIngredient = _dbContext.Ingredients
                    .AsNoTracking()
                    .FirstOrDefault(i => i.Name == ingredientDTO.IngredientName);

                if (existingIngredient == null)
                {
                    throw new InvalidOperationException($"Ingredient '{ingredientDTO.IngredientName}' not found.");
                }

                if (!recipeIngredients.Any(ri => ri.IngredientId == existingIngredient.Id))
                {
                    recipeIngredients.Add(new RecipeIngredient
                    {
                        IngredientId = existingIngredient.Id,
                        Quantity = ingredientDTO.Quantity,
                        Unit = ingredientDTO.Unit,
                    });
                }
            }

            return recipeIngredients;
        }

        private List<Comment> MapComments(IEnumerable<CommentDTO> commentDTOs)
        {
            return commentDTOs?.Select(comment =>
            {
                if (comment == null)
                {
                    throw new ArgumentNullException(nameof(comment), "Comment data cannot be null.");
                }

                return new Comment
                {
                    Id = comment.Id,
                    User = _userService.GetByUsername(comment.User),
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                };
            }).ToList() ?? new List<Comment>();
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
    

