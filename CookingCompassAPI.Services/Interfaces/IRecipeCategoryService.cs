using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IRecipeCategoryService
    {

        List<RecipeCategory> GetAll();

        RecipeCategory GetById (int id);

        RecipeCategory SaveRecipeCategory (RecipeCategory recipeCategory);

        void RemoveRecipeCategory (int id);
    }
}
