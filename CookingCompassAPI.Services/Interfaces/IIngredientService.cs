using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IIngredientService
    {

        List<IngredientDTO> GetAll();

        void SaveIngredient (IngredientDTO ingredientDTO);

        void RemoveIngredient (int id);
    }
}
