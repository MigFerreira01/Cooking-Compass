using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface IRatingService
    {

        List<Rating> GetAll();

        Rating GetById (int id);

        Rating SaveRating (Rating rating);

        void RemoveRating (int id);
    }
}
