using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {

        List<User> GetAll();

        User GetById (int id);  

        User Add (User user);

        User GetByUsername (string username);

        bool GetAny (int id);

        User Update (User user);    

        void Remove (User user);


    }
}
