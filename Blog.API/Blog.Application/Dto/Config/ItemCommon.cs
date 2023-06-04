using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto.Config
{
    public class ItemCommon
    {
        /// <summary>
        /// 默认不删除
        /// </summary>
        public bool? Deleted { get; set; } = false;
    }
}
