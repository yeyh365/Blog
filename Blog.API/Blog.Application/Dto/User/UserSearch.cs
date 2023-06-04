using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    /// <summary>
    /// 用户查询条件
    /// </summary>
    public class UserSearch : SearchCommon
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? Name { get; set; } = "";
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; } = "";
        /// <summary>
        /// 职务
        /// </summary>
        public string? Post { get; set; } = "";
    }
}
