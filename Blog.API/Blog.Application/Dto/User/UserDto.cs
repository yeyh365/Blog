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
        /// <summary>
        /// 省市区权限
        /// </summary>
        public InteractionDtoNum InteractionNum { get; set; }
    }
    public class InteractionDtoNum {
        public int? CollectionNum { get; set; } = 0;
        public int? FansNum { get; set; } = 0;
        public int? FollowNum { get; set; } = 0;
        public int? ArticleNum { get; set; } = 0;

        public int? MaterialNum { get; set; } = 0;




    }

}
