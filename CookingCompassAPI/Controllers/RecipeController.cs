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

        public Recipe SaveRecipe (Recipe recipe)
        {
            return _recipeService.SaveRecipe(recipe);
        }

        [HttpDelete("{id}")]
        public void DeleteRecipe (int id) 
        {
            _recipeService.RemoveRecipe(id);
        }
    }
}
