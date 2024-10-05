using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCompassAPI.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {

        private readonly DbSet<Comment> _dbSet;


        public CommentRepository(CookingCompassApiDBContext cookingCompassApiDBContext)
        {
            _dbSet = cookingCompassApiDBContext.Set<Comment>();
        }

        public List<Comment> GetAll()
        {
            return _dbSet.ToList(); // SELECT * FROM Comments;
        }

        public Comment GetById(int id) 
        {
            return _dbSet.FirstOrDefault(comment => comment.Id == id);
        }

        public bool GetAny(int id) 
        {
            return _dbSet.Any(comment => comment.Id == id);
        }

        public Comment Add (Comment comment) 
        {
            
            _dbSet.Add(comment);

            return comment;

        }

        public Comment Update (Comment comment) 
        {
            
            _dbSet.Update(comment);

            return comment;

        }

        public void Remove (Comment comment)
        {
            _dbSet.Remove(comment);
        }
    }
}
