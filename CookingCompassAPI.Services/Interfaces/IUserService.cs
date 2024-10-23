using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO;
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

        Task<UserDTO> GetByNameAsync (string username);

        UserDTO GetUserWithRecipes (string username);

        Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO);

        Task<SignInResult> LoginUserAsync(LoginResponseDTO loginResponseDTO);

        Task<IEnumerable<UserDTO>> GetUsersAsync();
    }
}
