using Blog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    /// <summary>
    /// lianxiwome表
    /// </summary>
    [Table("comment", Schema = "dbo")]
    public class Comment : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column("articleid")]
        public int? ArticleId { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        [Column("content")]
        public string? Content { get; set; } = string.Empty;

        /// <summary>
        /// 来自评论的用户id
        /// </summary>
        [Column("fromuserid")]
        public int? FromUserId { get; set; }
        /// <summary>
        /// 二级回复的评论id
        /// </summary>
        [Column("conparentkey")]
        public int? ConParentkey { get; set; }
        /// <summary>
        /// 回复人id
        /// </summary>
        [Column("replyuserid")]
        public int? ReplyUserId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; }

    }
}
