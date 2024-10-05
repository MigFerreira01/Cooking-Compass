namespace CookingCompassAPI.Domain
{
    public class Ingredient
    {

        public int Id { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public string Name { get; set; }

        public int RecipeId { get; set; }

        //public Recipe Recipe { get; set; }

    }
}
