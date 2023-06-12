using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class InteractionSearch:SearchCommon
    {
        public int? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? TypeName { get; set; }
    }
}
