using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPlaning.Core.Helper
{
    public class SHA1Helper
    {
        public static string SHA1Encrypt(string source, bool isReplace = true, bool isToLower = false)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(source));
            string shaStr = BitConverter.ToString(hash);
            if (isReplace)
            {
                shaStr = shaStr.Replace("-", "");
            }
            if (isToLower)
            {
                shaStr = shaStr.ToLower();
            }
            return shaStr;
        }
    }
}
