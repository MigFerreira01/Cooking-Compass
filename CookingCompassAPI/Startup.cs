using CookingCompassAPI.Services.Authentication.Token;
using System.Security.Cryptography;

namespace CookingCompassAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Generate a key if it doesn't exist
            EnsureJwtKeyIsPresent();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
            // Other service configurations
        }

        private void EnsureJwtKeyIsPresent()
        {
            // Get the path to appsettings.json
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            // Load existing configuration
            var jsonConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Check if the Jwt:Key exists
            if (string.IsNullOrWhiteSpace(jsonConfig["Jwt:Key"]))
            {
                // Generate a new key
                string newKey = GenerateSecureKey();

                // Read the existing appsettings.json content
                var jsonContent = File.ReadAllText(appSettingsPath);

                // Create a new configuration JSON object
                var newJsonContent = jsonContent.Replace(
                    "\"Jwt\": {",
                    $"\"Jwt\": {{\n    \"Key\": \"{newKey}\",");

                // Write the updated configuration back to the appsettings.json file
                File.WriteAllText(appSettingsPath, newJsonContent);
            }
        }

        public static string GenerateSecureKey(int size = 32) // Size in bytes
        {
            var key = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return Convert.ToBase64String(key); // Return as Base64 string
        }
    }
}
