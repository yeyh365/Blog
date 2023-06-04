namespace Blog.Domain.Entities
{
    using Blog.Domain.Repositories;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Blog.Domain.Enums;

    /// <summary>
    /// 管理员表
    /// </summary>
    [Table("user", Schema = "dbo")]
    public class User : LogicDeletableEntity, IEntitiy<int>
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
        /// 用户密码
        /// </summary>
        [Column("password")]
        public string? PassWord { get; set; } = string.Empty;
        /// <summary>
        /// 头像
        /// </summary>
        [Column("photo")]
        public string? Photo { get; set; } = string.Empty;

        /// <summary>
        /// 职务
        /// </summary>
        [Column("post")]
        public string? Post { get; set; } = string.Empty;

        /// <summary>
        /// 省
        /// </summary>
        [Column("province")]
        public string? Province { get; set; } = string.Empty;

        /// <summary>
        /// 市
        /// </summary>
        [Column("city")]
        public string? City { get; set; } = string.Empty;

        /// <summary>
        /// 区/县
        /// </summary>
        [Column("county")]
        public string? County { get; set; } = string.Empty;
    }
}