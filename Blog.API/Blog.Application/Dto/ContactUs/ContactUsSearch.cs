using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    /// <summary>
    /// 用户查询条件
    /// </summary>
    public class ContactUsSearch : SearchCommon
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; } = string.Empty;
    }
}
