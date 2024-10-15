using CookingCompassAPI.Domain;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookingCompassAPI.Controllers
{
    [Route("CookingCompassAPI/[Controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService) 
        {
            
            _recipeService = recipeService;

        }

        [HttpGet("{id}")]

        public Recipe GetById (int id) 
        {
            return _recipeService.GetById(id);
        }

        //[HttpGet("{category}")]

        //public List<Recipe> GetByCategory(string category)
        //{
        //    return _recipeService.GetByCategory(category);
        //}

        //[HttpGet("{difficultyLevel}")]

        //public List<Recipe> GetByDifficulty(string difficcultyLevel)
        //{
        //    return _recipeService.GetByDifficulty(difficcultyLevel);
        //}

        //[HttpGet("{by-ingredient}")]

        //public List<Recipe> GetByIngredient([FromQuery] List<string> ingredients)
        //{
        //    return _recipeService.GetByIngredient (ingredients);
        //}

        [HttpGet]
        public List<Recipe> GetAll()
        {
            return _recipeService.GetAll();
        }

        [HttpPost]
        public IActionResult SaveRecipe([FromBody] RecipeWithIngredientsDto recipeWithIngredients)
        {
            if (recipeWithIngredients == null || recipeWithIngredients.Recipe == null || recipeWithIngredients.Ingredients == null)
            {
                return BadRequest("Invalid recipe or ingredients data.");
            }

            var recipe = new Recipe
            {
                Id = recipeWithIngredients.Recipe.Id,
                Name = recipeWithIngredients.Recipe.Name,
                Ingredients = recipeWithIngredients.Ingredients.Select(ingredient => new Ingredient
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit,
                }).ToList()
            };

            var savedRecipe = _recipeService.SaveRecipe(recipe, recipe.Ingredients.ToList());

            if (recipe == null)
            {
                return StatusCode(500, "A problem occurred while saving the recipe.");
            }

            return Ok(recipe); // Return the saved recipe as a response
        }


        [HttpDelete("{id}")]
        public void DeleteRecipe (int id) 
        {
            _recipeService.RemoveRecipe(id);
        }
    }
}
