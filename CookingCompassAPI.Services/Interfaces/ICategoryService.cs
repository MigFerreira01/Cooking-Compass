using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();

        Category GetByName(string name);

        Category GetById(int id);

        Category UpdateCategory (Category category);

        void RemoveCategory (int id);
    }
}
