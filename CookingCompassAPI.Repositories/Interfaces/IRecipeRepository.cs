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

        Task<Recipe> GetByIdAsync(int id);

        Recipe Add (Recipe recipe);

        bool GetAny(int id);

        void Remove(Recipe recipe);


    }
}