using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Domain.DTO_s;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IRecipeService
    {

        Task<RecipeDTO> AddRecipeAsync(RecipeDTO recipeDTO);

        List<RecipeDTO> GetAllRecipes();

        Task<RecipeDTO> GetRecipeByIdAsync(int id);

        Task AddCommentsToRecipeAsync(int recipeId, List<CommentDTO> commentDTOs);

        Task<bool> RemoveRecipeAsync(int id);
    }
}
