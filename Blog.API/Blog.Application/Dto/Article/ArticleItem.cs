using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.Dto.Config;

namespace Blog.Application.Dto
{
    public class ArticleItem: ItemCommon
    {
        public int? Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ArticleId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string? ArticleContent { get; set; } = string.Empty;

        /// <summary>
        /// md文件地址
        /// </summary>
        public string? ArticleContentUrl { get; set; } = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public string? ArticleTitle { get; set; } = string.Empty;
        /// <summary>
        /// 封面文件地址
        /// </summary>
        public string? CoverImgId { get; set; } = string.Empty;
        /// <summary>
        /// 封面图片地址
        /// </summary>
        public string? CoverImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string? IsAppeal { get; set; } = string.Empty;
        /// <summary>
        /// 发布用户id
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string? Status { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; } = string.Empty;

    }
}
