using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Implementations;
using System.Security.Cryptography;

namespace CookingCompassAPI.ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Generate and print a new Base64-encoded key
            string base64Key = GenerateSecureKey();
            Console.WriteLine("Your new Base64-encoded key is: " + base64Key);
        }

        public static string GenerateSecureKey(int size = 32) // Size in bytes
        {
            var key = new byte[size]; // Create a byte array of specified size
            using (var rng = RandomNumberGenerator.Create()) // Use a secure random number generator
            {
                rng.GetBytes(key); // Fill the byte array with random bytes
            }
            return Convert.ToBase64String(key); // Convert the byte array to a Base64 string
        }
    }
}
 