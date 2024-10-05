using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface IIngredientRepository
    {

        List<Ingredient> GetAll();

        Ingredient GetById (int id);  

        Ingredient Add (Ingredient ingredient);

        bool GetAny (int id);

        Ingredient Update (Ingredient ingredient);    

        void Remove (Ingredient ingredient);


    }
}
