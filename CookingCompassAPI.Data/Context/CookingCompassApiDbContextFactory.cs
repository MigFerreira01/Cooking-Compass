using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using CookingCompassAPI.Domain;
using System.Net;

namespace CookingCompassAPI.Data.Context
{
    public class CookingCompassApiDbContextFactory : IDesignTimeDbContextFactory<CookingCompassApiDBContext>
    {
        public CookingCompassApiDBContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
                .Build();

            // Create DbContextOptionsBuilder and configure it with the connection string
            var optionsBuilder = new DbContextOptionsBuilder<CookingCompassApiDBContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Use SQL Server
            optionsBuilder.UseSqlServer(connectionString);

            return new CookingCompassApiDBContext(optionsBuilder.Options);
        }
    }
}
