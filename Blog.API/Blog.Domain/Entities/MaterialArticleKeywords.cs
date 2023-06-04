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
    /// 字典表
    /// </summary>
    [Table("materialarticlekeywords", Schema = "dbo")]
    public class MaterialArticleKeywords : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        [Column("type")]
        public string? Type { get; set; } = string.Empty;
        /// <summary>
        /// 名字
        /// </summary>
        [Column("materialarticleid")]
        public int? MateriaArticlelId { get; set; }

        /// <summary>
        /// key
        /// </summary>
        [Column("keywordsid")]
        public int? KeywordsId { get; set; }

        /// <summary>
        /// 内容备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;

    }
}
