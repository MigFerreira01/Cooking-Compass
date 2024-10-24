using CookingCompassAPI.Domain.DTO_s;
using CookingCompassAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Translates
{
    public class TranslateComment
    {
        public Comment MapComment (CommentDTO commentDTO)
        {
            return new Comment
            {
                Id = commentDTO.Id,
                Content = commentDTO.Content,
                CreatedAt = commentDTO.CreatedAt,

            };

        }

        public CommentDTO MapCommentDTO(Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                User = comment.User?.Name,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };
        }
    }
}
