using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class LoginItem
    {
        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey { get; set; } = "";
        /// <summary>
        /// encryptedData
        /// </summary>

        public string encryptedData { get; set; } = "";
        /// <summary>
        /// iv
        /// </summary>

        public string iv { get; set; } = "";
    }
}
