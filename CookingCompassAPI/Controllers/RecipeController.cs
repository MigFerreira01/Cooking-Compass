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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound(new { message = $"Recipe with id {id} not found." });
            }

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeDTO recipeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _recipeService.AddRecipeAsync(recipeDTO);
                    return CreatedAtAction(nameof(GetRecipe), new { id = result.Id }, result);

                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRecipe(int id)
        {
            try
            {
                var success = await _recipeService.RemoveRecipeAsync(id);

                if (!success)
                {
                    return NotFound(new { message = $"Recipe with id {id} not found." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the recipe.", details = ex.Message });
            }
        }
    }
}
