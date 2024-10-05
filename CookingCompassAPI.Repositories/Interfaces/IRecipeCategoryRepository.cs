using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface IRecipeCategoryRepository
    {

        List<RecipeCategory> GetAll();

        RecipeCategory GetById (int id);  

        RecipeCategory Add (RecipeCategory recipeCategory);

        bool GetAny (int id);

        RecipeCategory Update (RecipeCategory recipeCategory);    

        void Remove (RecipeCategory recipeCategory);


    }
}
