using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class WeChatResult
    {
        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; } = "";
        /// <summary>
        /// session_key
        /// </summary>

        public string session_key { get; set; } = "";
        /// <summary>
        /// unionid
        /// </summary>

        public string unionid { get; set; } = "";
        /// <summary>
        /// errcode
        /// </summary>

        public string errcode { get; set; } = "";
        /// <summary>
        /// errmsg
        /// </summary>

        public string errmsg { get; set; } = "";
    }
}
