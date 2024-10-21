using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
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

        [HttpGet]
        public List<RecipeDTO> GetAll()
        {
            return _recipeService.GetAll();
        }

        [HttpPost]

        public void AddRecipe(RecipeDTO recipeDTO) 
        {
            _recipeService.AddRecipe(recipeDTO);
        }
    


        [HttpDelete("{id}")]
        public void RemoveRecipe (int id) 
        {
            _recipeService.RemoveRecipe(id);
        }
    }
}
