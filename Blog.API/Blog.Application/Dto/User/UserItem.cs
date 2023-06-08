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
    public class UserItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; } 

        /// <summary>
        /// 中文姓名
        /// </summary>
        public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// 用户密码
        /// </summary>
        public string? PassWord { get; set; } = string.Empty;
        /// <summary>
        /// 职务
        /// </summary>
        public string? Photo { get; set; } = string.Empty;


        /// <summary>
        /// 职务
        /// </summary>
        public string? Post { get; set; } = string.Empty;

        /// <summary>
        /// 省
        /// </summary>
        public string? Province { get; set; } = string.Empty;

        /// <summary>
        /// 市
        /// </summary>
        public string? City { get; set; } = string.Empty;

        /// <summary>
        /// 区/县
        /// </summary>
        public string? County { get; set; } = string.Empty;
        /// <summary>
        /// 区/县
        /// </summary>
        public int? Code { get; set; }
    }

    public class UserLogin
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? Account { get; set; } = string.Empty;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? Email { get; set; } = string.Empty;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? PassWord { get; set; } = string.Empty;
    }
}
