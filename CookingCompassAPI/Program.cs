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

            // Configuração do Serviço do Swagger
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

            // Registo de repositórios
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();


            // Registo de serviços
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

            // Configuração da Interface Gráfica do Swagger
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("CookingCompassAPI/Swagger.json", "Cooking Compass");
            });

            app.Run();
        }
    }
}
