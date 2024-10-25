using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;

namespace CookingCompassAPI.Services.Translates
{
    public class TranslateRecipe
    { 

        private readonly TranslateUser _translateUser;

        private IIngredientService _ingredientService;

        private IIngredientRepository _ingredientRepository;

        private CookingCompassApiDBContext _dbContext;

        public TranslateRecipe(TranslateUser translateUser, IIngredientService ingredientService, IIngredientRepository ingredientRepository, CookingCompassApiDBContext dBContext)
        {
            _translateUser = translateUser;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
            _dbContext = dBContext;
        }

        public Recipe MapRecipe (RecipeDTO recipeDTO)
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
                Difficulty = GetDifficultyLevelByString(recipeDTO.Difficulty),
                Category = GetRecipeCategoryByString(recipeDTO.Category),
                Status = GetApprovalStatusByString(recipeDTO.Status),
                ImageUrl= recipeDTO.ImageURL,
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

        public RecipeDTO MapRecipeDTO(Recipe recipe) => new RecipeDTO
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Duration = recipe.Duration,
            Ingredients = recipe.RecipeIngredients?.Select(ing => new RecipeIngredientDTO
            {
                IngredientID = ing.Ingredient?.Id ?? 0,
                IngredientName = ing.Ingredient.Name,
                Quantity = ing.Quantity,
                Unit = ing.Unit,
            }).ToList() ?? new List<RecipeIngredientDTO>(),
            User = recipe.User?.Name ?? "Unknown",
            Category = recipe.Category.ToString() ?? "Unknown",
            Difficulty = recipe.Difficulty.ToString() ?? "Unknown",
            Status = recipe.Status.ToString() ?? "Unknown",
            ImageURL=recipe.ImageUrl
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
    

