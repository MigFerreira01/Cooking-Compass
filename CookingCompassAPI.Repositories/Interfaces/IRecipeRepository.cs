using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface IRecipeRepository
    {

        List<Recipe> GetAll();

        Recipe GetById(int id);

        bool GetAny(int id);

        void Remove(Recipe recipe);


    }
}