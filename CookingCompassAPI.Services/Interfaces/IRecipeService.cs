using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IRecipeService
    {

        Task<Recipe> AddRecipeAsync(RecipeDTO recipeDTO);

        Task<RecipeDTO> GetRecipeByIdAsync(int id);

        Task<bool> RemoveRecipeAsync(int id);
    }
}
