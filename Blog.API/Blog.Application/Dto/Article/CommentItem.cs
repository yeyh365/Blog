using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class CommentItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public int? ArticleId { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string? Content { get; set; } = string.Empty;

        /// <summary>
        /// 来自评论的用户id
        /// </summary>
        public int? FromUserId { get; set; }
        /// <summary>
        /// 二级回复的评论id
        /// </summary>
        public int? ConParentkey { get; set; }
        /// <summary>
        /// 回复人id
        /// </summary>
        public int? ReplyUserId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}
