using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Services.Implementations
{
    public class RecipeService :IRecipeService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IRecipeRepository _recipeRepository;

        private readonly TranslateRecipe _translateRecipe;

        private readonly TranslateUser _translateUser;

        private readonly IUserService _userService;

        private readonly ILogger<RecipeService> _logger;

        public RecipeService (CookingCompassApiDBContext cookingApiDBContext, IRecipeRepository recipeRepository, ILogger<RecipeService> logger, TranslateRecipe translateRecipe, IUserService userService, TranslateUser translateUser)
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

            List <RecipeDTO> recipeDTOs = recipes.Select(_translateRecipe.MapRecipeDTO).ToList();

            return recipeDTOs;
        }

        public void AddRecipe(RecipeDTO recipeDTO)
        {
            var existingUser = _userService.GetByUsername(recipeDTO.User);

            if(existingUser == null) 
            {
                throw new ArgumentException($"User '{recipeDTO.User}' does not exist.");
            }

            var recipe = _translateRecipe.MapRecipe(recipeDTO);

            recipe.User = existingUser;

            _recipeRepository.Add(recipe);
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

        private readonly TranslateUser _translateUser;

        private IIngredientService _ingredientService;

        private IIngredientRepository _ingredientRepository;

        public TranslateRecipe(IUserService userService, TranslateUser translateUser, IIngredientService ingredientService, IIngredientRepository ingredientRepository)
        {
            _userService = userService;
            _translateUser = translateUser;
            _ingredientService = ingredientService;
            _ingredientRepository = ingredientRepository;
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

                RecipeIngredients = recipeDTO.Ingredients?.Select(ingredientDTO =>
                {
                    if (ingredientDTO == null)
                    {
                        throw new ArgumentNullException(nameof(ingredientDTO), "Ingredient data cannot be null.");
                    }

                    var existingIngredient = _ingredientRepository.GetAll()
                        .FirstOrDefault(ing => ing.Name.Equals(ingredientDTO.IngredientName, StringComparison.OrdinalIgnoreCase));

                    Ingredient ingredient;

                    if (existingIngredient != null)
                    {
                        ingredient = existingIngredient;
                    }
                    else
                    {
                        ingredient = new Ingredient
                        {
                            Name = ingredientDTO.IngredientName
                        };
                    }

                    return new RecipeIngredient
                    {
                        Ingredient = ingredient,
                        Quantity = ingredientDTO.Quantity,
                        Unit = ingredientDTO.Unit,
                    };

                }).ToList() ?? new List<RecipeIngredient>(),

                User = _userService.GetByUsername(recipeDTO.User),

                Difficulty = GetDifficultyLevelByString(recipeDTO.Difficulty),
                Category = GetRecipeCategoryByString(recipeDTO.Category),
                Status = GetApprovalStatusByString(recipeDTO.Status),

                Comments = recipeDTO.Comments?.Select(comment =>
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

                }).ToList() ?? new List<Comment>(),
            };
        }



        public RecipeDTO MapRecipeDTO (Recipe recipe) => new RecipeDTO
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
