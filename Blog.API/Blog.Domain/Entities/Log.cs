namespace Blog.Domain.Entities
{
    using Blog.Domain.Repositories;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Blog.Domain.Enums;

    /// <summary>
    /// 日志
    /// </summary>
    [Table("log", Schema = "dbo")]
    public class Log : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 中文姓名
        /// </summary>
        [Column("name")]
        public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("account")]
        public string? Account { get; set; } = string.Empty;

        /// <summary>
        /// 请求IP
        /// </summary>
        [Column("ip")]
        public string? IP { get; set; } = string.Empty;
        /// <summary>
        /// Type
        /// </summary>
        [Column("type")]
        public string? Type { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string? Description { get; set; } = string.Empty;
    }
}