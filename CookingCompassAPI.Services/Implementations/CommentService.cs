using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class CommentService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private ICommentRepository _commentRepository;

        public CommentService (CookingCompassApiDBContext cookingApiDBContext, ICommentRepository commentRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _commentRepository = commentRepository;
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetById (int id) 
        {
            return _commentRepository.GetById(id);
        }


        public Comment SaveComment (Comment comment)
        {
            bool commentExists = _commentRepository.GetAny(comment.Id);

            if (!commentExists)
            {
              comment = _commentRepository.Add(comment);
            }
            else
            {
                comment = _commentRepository.Update(comment);
            }

            return comment;
        }

        public void RemoveComment (int id)
        {
            Comment commentResult = _commentRepository.GetById(id);

            if (commentResult != null)
            {
                _commentRepository.Remove(commentResult);

                _cookingApiDBContext.SaveChanges();
            }
        }

    }




}
