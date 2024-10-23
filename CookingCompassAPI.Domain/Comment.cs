namespace CookingCompassAPI.Domain
{
    public class Comment
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public int RecipeId { get; set; }  
        public int UserId { get; set; }    
        public Recipe Recipe { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; } // Soft delete flag

    }
}
