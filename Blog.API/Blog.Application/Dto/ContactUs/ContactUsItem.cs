using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    /// <summary>
    /// 公司
    /// </summary>
    public class ContactUsItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

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
