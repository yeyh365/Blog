using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class MaterialSearch:SearchCommon
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string? MaterialName { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string? Language { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string? Direction { get; set; }
        /// <summary>
        /// 框架
        /// </summary>
        public string? Framework { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string? keywords { get; set; }
        public string? sort { get; set; }
    }
}
