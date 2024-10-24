using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Domain.DTO;
using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Repositories.Implementations;
using CookingCompassAPI.Repositories.Interfaces;
using CookingCompassAPI.Services.Interfaces;
using CookingCompassAPI.Services.Translates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class CommentService : ICommentService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private ICommentRepository _commentRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserRepository _userRepository;

        private readonly TranslateComment _translateComment;

        public CommentService (CookingCompassApiDBContext cookingApiDBContext, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, TranslateComment translateComment)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor; 
            _userRepository = userRepository;
            _translateComment = translateComment;
            
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetById (int id) 
        {
            return _commentRepository.GetById(id);
        }


        public async Task<CommentDTO> SaveCommentAsync (CommentDTO commentDTO)
        {
            using var transaction = await _cookingApiDBContext.Database.BeginTransactionAsync();

            try
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;

                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentException("User is not authenticated.");
                }

                var existingUser = _userRepository.GetByUsername(username);
                if (existingUser == null)
                {
                    throw new ArgumentException($"User '{username}' does not exist.");
                }

                var comment = _translateComment.MapComment(commentDTO);

                comment.User = existingUser;
                _commentRepository.Add(comment);

                await _cookingApiDBContext.SaveChangesAsync();
                await transaction.CommitAsync();

                commentDTO = _translateComment.MapCommentDTO(comment);

                return commentDTO;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<CommentDTO>> GetCommentsByRecipeIdAsync(int recipeId)
        {
            var comments = await _commentRepository.GetByRecipeIdAsync(recipeId);

            if (comments == null || !comments.Any())
            {
                return new List<CommentDTO>();
            }
            var commentDTOs = comments.Select(comment => _translateComment.MapCommentDTO(comment)).ToList();

            return commentDTOs;
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
