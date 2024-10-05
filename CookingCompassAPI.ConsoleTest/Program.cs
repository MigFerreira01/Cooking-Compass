using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Implementations;

namespace CookingCompassAPI.ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CookingCompassApiDBContext cookingCompassApiDBContext = new CookingCompassApiDBContext();

            UserRepository userRepository = new UserRepository(cookingCompassApiDBContext);

            PrintUsers(userRepository);

            User userToInsert = new User();

            Console.WriteLine("Insira um utilizador;");
            Console.Write("Nome: ");
            userToInsert.Name = Console.ReadLine();
            Console.Write("Email: ");
            userToInsert.Email = Console.ReadLine();
            Console.Write("Password: ");
            userToInsert.Password = Console.ReadLine();

            userRepository.Add(userToInsert);

            cookingCompassApiDBContext.SaveChanges();   

            PrintUsers(userRepository);
            Console.WriteLine($"Globalization Invariant Mode: {Environment.GetEnvironmentVariable("DOTNET_SYSTEM_GLOBALIZATION_INVARIANT")}");

        }

        public static void PrintUsers(UserRepository userRepository)
        {

            List<User> users = userRepository.GetAll();

            foreach (User user in users)
            {
                Console.WriteLine(user.Name);
            }

        }
    }
}
 