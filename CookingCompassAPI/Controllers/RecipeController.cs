using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingCompassAPI.Controllers
{
    [Route("CookingCompassAPI/[Controller]")]
    [Authorize]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService) 
        {
            
            _recipeService = recipeService;

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound(new { message = $"Recipe with id {id} not found." });
            }

            return Ok(recipe);
        }

        [HttpGet]
        [AllowAnonymous]

        public List<RecipeDTO> GetAllRecipes() 
        { 
            return _recipeService.GetAllRecipes();
        }

        [HttpPost("{recipeId}")]

        public async Task<IActionResult> UpdateRecipe (int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _recipeService.UpdateRecipeAsync(id);

                    return CreatedAtAction(nameof(GetRecipe), new { id = result.Id }, result);

                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

            return BadRequest(ModelState);

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
