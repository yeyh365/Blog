using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class InteractionItme
    {
        public int? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ArticleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool? Status { get; set; } = true;

        /// <summary>
        /// 标题
        /// </summary>
        public int? AttentionUserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Remark { get; set; } = string.Empty;
    }
}
