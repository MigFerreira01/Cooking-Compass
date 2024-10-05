using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Repositories.Interfaces
{
    public interface ICommentRepository
    {

        List<Comment> GetAll();

        Comment GetById (int id);  

        Comment Add (Comment comment);

        bool GetAny (int id);

        Comment Update (Comment comment);    

        void Remove (Comment comment);


    }
}
