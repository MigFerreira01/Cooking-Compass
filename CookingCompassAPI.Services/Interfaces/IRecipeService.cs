using CookingCompassAPI.Domain.DTO;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IRecipeService
    {

        Task<RecipeDTO> AddRecipeAsync(RecipeDTO recipeDTO);

        Task<RecipeDTO> GetRecipeByIdAsync(int id);

        Task<bool> RemoveRecipeAsync(int id);
    }
}
