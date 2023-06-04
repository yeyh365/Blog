using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Helper
{
    public class MD5Helper
    {
        /// <summary>
        /// <summary>
        ///     MD5 加密 字符串 
        /// </summary>
        /// <param name="input">用户输入的内容</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string Md5Method(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] bytes = Encoding.Default.GetBytes(input);

            byte[] bytesMd5 = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytesMd5.Length; i++)
            {
                sb.Append(bytesMd5[i].ToString("x2")); //32位小写
            }

            return sb.ToString();
        }
    }
}
