using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Authentication;
using CookingCompassAPI.Services.Authentication.PasswordHash;
using CookingCompassAPI.Services.Authentication.Token;
using CookingCompassAPI.Services.Implementations;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

using System.Text.Json.Serialization;

namespace CookingCompassAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ADD LOGs
            var logDirectory = "C:/ProgramData/CookingCompassLogs/";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                Log.Information("Created log directory: {LogDirectory}", logDirectory);
            }
            //CONFIG SERILOG
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(logDirectory, "CookingCompass.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting application...");

                var builder = WebApplication.CreateBuilder(args);

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File(Path.Combine(logDirectory, "CookingCompass.log"), rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                builder.Host.UseSerilog();


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

                
                builder.Services.AddScoped<CookingCompassApiDBContext>();

                
                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
                builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
                
                builder.Services.AddScoped<TranslateUser>();


                builder.Services.AddScoped<TranslateRecipe>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<IRecipeService, RecipeService>();
                builder.Services.AddScoped<IIngredientService, IngredientService>();
                builder.Services.AddScoped<AuthService>();
                builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
                builder.Services.AddScoped<ITokenService, TokenService>();
                builder.Services.AddSingleton<ITokenService, TokenService>();

                builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {

                        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

                    });

                var app = builder.Build();

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

               
                app.UseSwagger().UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("CookingCompassAPI/Swagger.json", "Cooking Compass");
                });

                app.MapControllers();

                Log.Information("Application is running.");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "App fatal error");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }

}