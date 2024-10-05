namespace CookingCompassAPI.Domain
{
    public class Comment
    {

        public int Id { get; set; } 

        public string Text { get; set; }

        public User Author { get; set; }

        public int RecipeId { get; set; }

        public DateTime CommentDate { get; set; }

    }
}
