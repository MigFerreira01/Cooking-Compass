﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }   

        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public double Quantity { get; set; }

        public string Unit {  get; set; }
    }
}