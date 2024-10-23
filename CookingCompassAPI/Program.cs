using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Implementations;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CookingCompassAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<CookingCompassApiDBContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<CookingCompassApiDBContext>()
            .AddDefaultTokenProviders();

            // Add Swagger services
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("CookingCompassAPI", new OpenApiInfo()
                {
                    Title = "Cooking Compass",
                    Version = "1.0"
                });
            });

            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<TranslateUser>();
            builder.Services.AddScoped<TranslateRecipe>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<IIngredientService, IngredientService>();

            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<SignInManager<User>>();

            builder.Services.AddControllers();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/CookingCompassAPI/swagger.json", "Cooking Compass");
            });

            
            app.MapControllers();

            app.Run();
        }
    }
}