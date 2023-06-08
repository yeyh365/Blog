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
    /// 推荐物品表
    /// </summary>
    [Table("material", Schema = "dbo")]
    public class Material : LogicDeletableEntity, IEntitiy<int>
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
        [Column("browsenum")]
        public int? BrowseNum { get; set; }

        /// <summary>
        /// key
        /// </summary>
        [Column("likenum")]
        public int? LikeNum { get; set; }
        /// <summary>
        /// key
        /// </summary>
        [Column("materialcover")]
        public string? MaterialCover { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("materialcoverid")]
        public int? MaterialCoverid { get; set; }
        /// <summary>
        /// key
        /// </summary>
        [Column("materialdescribe")]
        public string? MaterialDescribe { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("materialdetails")]
        public string? MaterialDetails { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("materiallink")]
        public string? MaterialLink { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [Column("materialname")]
        public string? MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// Slot
        /// </summary>
        [Column("status")]
        public int? Status { get; set; }

        /// <summary>
        /// ParentKey
        /// </summary>
        [Column("userid")]
        public int? UserId { get; set; }
        /// <summary>
        /// 内容备注
        /// </summary>
        [Column("bannerurl")]
        public string? BannerUrl { get; set; } = string.Empty;
        /// <summary>
        /// 内容备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;
    }
}