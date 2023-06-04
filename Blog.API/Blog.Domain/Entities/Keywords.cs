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
    [Table("keywords", Schema = "dbo")]
    public class Keywords : LogicDeletableEntity, IEntitiy<int>
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
        [Column("name")]
        public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// 名字
        /// </summary>
        [Column("typeid")]
        public string? TypeId { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("typename")]
        public string? TypeName { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("parentkey")]
        public int? ParentKey { get; set; }
        /// <summary>
        /// key
        /// </summary>
        [Column("sort")]
        public int? Sort { get; set; } 
        /// <summary>
        /// key
        /// </summary>
        [Column("describe")]
        public string? Describe { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("imgurl")]
        public string? ImgUrl { get; set; } = string.Empty;

        /// <summary>
        /// 内容备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;

    }
}
