using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface IRatingRepository
    {

        List<Rating> GetAll();

        Rating GetById (int id);  

        Rating Add (Rating rating);

        bool GetAny (int id);

        Rating Update (Rating rating);    

        void Remove (Rating rating);


    }
}
