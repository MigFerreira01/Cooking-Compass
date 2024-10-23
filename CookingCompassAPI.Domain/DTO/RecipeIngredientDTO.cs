using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain.DTO_s
{
    public class RecipeIngredientDTO
    {

        public int IngredientID { get; set; }  

        public string IngredientName { get; set; }

        public double Quantity { get; set; }

        public string Unit {  get; set; }

    }
}
