using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class CommentDto:Comment
    {
        public User FromUserInfo { get; set; }
        public User ReplyUserInfo { get; set; }
        public List<CommentDto> Children { get; set; }

    }
}
