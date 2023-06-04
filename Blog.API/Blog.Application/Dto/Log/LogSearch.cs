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
    public class LogSearch : SearchCommon
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Account { get; set; } = "";

        /// <summary>
        /// 公司名
        /// </summary>
        public string? OptionType { get; set; } = "";
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; } 

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; } 
    }
}
