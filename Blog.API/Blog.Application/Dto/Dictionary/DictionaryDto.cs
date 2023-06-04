using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    /// <summary>
    /// 公司
    /// </summary>
    public class DictionaryDto : Dictionary
    {
         public List<Keywords> Keywords { get; set; }
    }
}
