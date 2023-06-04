using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    /// <summary>
    /// 公司
    /// </summary>
    public class UserDto : User
    {
        /// <summary>
        /// 职务
        /// </summary>
        public string? PostName { get; set; } = string.Empty;

        /// <summary>
        /// 省市区权限
        /// </summary>
        public List<DictionaryDto> DicList { get; set; }
    }
}
