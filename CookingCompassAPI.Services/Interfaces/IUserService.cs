using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IUserService
    {

        List<UserDTO> GetAll();

        UserDTO GetById(int id);

        void SaveUser (UserDTO userDTO);

        void RemoveUser (int id);

        bool UserExists (string username); 

        User GetByUsername (string username);

        User GetUserWithRecipes (string username);
    }
}
