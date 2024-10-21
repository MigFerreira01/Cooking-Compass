using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Domain.DTO_s
{
    public class CommentDTO
    {

        public int Id { get; set; }

        public string User { get; set; }

        public string Content { get; set; }

        public  DateTime CreatedAt { get; set; }

    }
}
