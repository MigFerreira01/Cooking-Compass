using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Implementations;
using CookingCompassAPI.Services.Interfaces;
using Microsoft.OpenApi.Models;

using System.Text.Json.Serialization;

namespace CookingCompassAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura��o do Servi�o do Swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "CookingCompassAPI",
                    new OpenApiInfo()
                    {
                        Title = "Cooking Compass",
                        Version = "1.0"
                    });
            });

            // Registo da base de dados
            builder.Services.AddScoped<CookingCompassApiDBContext>();

            // Registo de reposit�rios
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();


            // Registo de servi�os
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();


            // Registo dos controladores
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configura��o da Interface Gr�fica do Swagger
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("CookingCompassAPI/Swagger.json", "Cooking Compass");
            });

            app.Run();
        }
    }
}
