using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Interfaces
{
    public interface ICommentService
    {

        List<Comment> GetAll();

        Comment GetById (int id);

        Task<CommentDTO> SaveCommentAsync(CommentDTO commentDTO);

        void RemoveComment (int id);
    }
}
