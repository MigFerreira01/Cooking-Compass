using CookingCompassAPI.Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain.DAL
{
    public class RecipeDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public string Status { get; set; }
        public List <CommentDTO> Comments { get; set; }
        public int Duration { get; set; } 
        public List<RecipeIngredientDTO> Ingredients { get; set; }

    }
}

