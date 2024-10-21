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

        List<RecipeDTO> GetAll();

        void AddRecipe (RecipeDTO recipeDTO);

        void RemoveRecipe (int id);
    }
}
