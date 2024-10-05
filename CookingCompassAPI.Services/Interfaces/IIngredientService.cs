using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IIngredientService
    {

        List<Ingredient> GetAll();

        Ingredient GetById (int id);

        Ingredient SaveIngredient (Ingredient ingredient);

        void RemoveIngredient (int id);
    }
}
