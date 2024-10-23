using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IUserService
    {

        UserDTO GetById(int id);

        void RemoveUser (int id);

        bool UserExists (string username); 

        Task<User> GetByNameAsync (string username);

        User GetUserWithRecipes (string username);

        Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO);

        Task<SignInResult> LoginUserAsync(LoginResponseDTO loginResponseDTO);

        Task<IEnumerable<UserDTO>> GetUsersAsync();
    }
}
