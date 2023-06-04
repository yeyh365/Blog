namespace Blog.Domain.Entities
{
    using Blog.Domain.Repositories;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Blog.Domain.Enums;

    /// <summary>
    /// lianxiwome表
    /// </summary>
    [Table("contactus", Schema = "dbo")]
    public class ContactUs : LogicDeletableEntity, IEntitiy<int>
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
        [Column("title")]
        public string? Title { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("phone")]
        public string? Phone { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string? Content { get; set; } = string.Empty;
    }
}