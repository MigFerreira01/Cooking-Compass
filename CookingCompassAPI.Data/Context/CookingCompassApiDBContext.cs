using CookingCompassAPI.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Data.Context
{
    public class CookingCompassApiDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Comment> Comments { get; set; }


        public CookingCompassApiDBContext(DbContextOptions<CookingCompassApiDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); // Optional: can be omitted
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Recipe)
               .WithMany(r => r.Comments) // Assuming Recipe has a collection of Comments
               .HasForeignKey(c => c.RecipeId)
               .OnDelete(DeleteBehavior.NoAction); // or NoAction depending on your preference

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany() // Assuming User does not have a collection of Comments
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // or NoAction depending on your preference
        }

    }
}
