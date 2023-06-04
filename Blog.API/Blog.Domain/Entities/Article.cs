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
    [Table("article", Schema = "dbo")]
    public class Article : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("articleid")]
        public int? ArticleId { get; set; } 

        /// <summary>
        /// 内容
        /// </summary>
        [Column("articlecontent")]
        public string? ArticleContent { get; set; } = string.Empty;

        /// <summary>
        /// md文件地址
        /// </summary>
        [Column("articlecontenturl")]
        public string? ArticleContentUrl { get; set; } = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        [Column("articletitle")]
        public string? ArticleTitle { get; set; } = string.Empty;
        /// <summary>
        /// 封面文件地址
        /// </summary>
        [Column("coverimgid")]
        public string? CoverImgId { get; set; } = string.Empty;
        /// <summary>
        /// 封面图片地址
        /// </summary>
        [Column("coverimgurl")]
        public string? CoverImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("isappeal")]
        public string? IsAppeal { get; set; } = string.Empty;
        /// <summary>
        /// 发布用户id
        /// </summary>
        [Column("userid")]
        public int? UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public string? Status { get; set; } = string.Empty;
        /// <summary>
        /// 发布用户id
        /// </summary>
        [Column("browsenum")]
        public int? BrowseNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;
    }
}
