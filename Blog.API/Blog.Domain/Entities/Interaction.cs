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
    ///互动表
    /// </summary>
    [Table("interaction", Schema = "dbo")]
    public class Interaction : LogicDeletableEntity, IEntitiy<int>
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
        [Column("typename")]
        public string? TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("articleid")]
        public int? ArticleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("userid")]
        public int? UserId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public bool? Status { get; set; } = true;

        /// <summary>
        /// 标题
        /// </summary>
        [Column("attentionuserid")]
        public int? AttentionUserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;
    }
}
