using Newtonsoft.Json;
using Blog.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Helper
{
    public class AESHelper
    {
        public static string Decrypt(string toDecrypt, string key)
        {
            try
            {
                var keyArray = Convert.FromBase64String(key);
                var toDecryptArray = Convert.FromBase64String(toDecrypt);
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Convert.FromBase64String(key);
                    aesAlg.IV = aesAlg.IV;
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;
                    var de = aesAlg.CreateDecryptor();
                    //解密数据
                    var decodeByteData = de.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                    //转换为字符串
                    string str = Encoding.UTF8.GetString(decodeByteData);
                    return str;
                };
            }
            catch (Exception ee)
            {
                string str = ee.Message;
                return str;
            }


        }
        /// <summary>  
        /// AES加密(无向量)  
        /// </summary>  
        /// <param name="encrypteStr">需要加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>密文</returns>  
        public static string AESEncrypt(byte[] Data, byte[] Key)
        {
            byte[] encryptedBytes = Data;
            byte[] bKey = Key;
            Aes Aes = Aes.Create();
            Aes.Mode = CipherMode.ECB;
            Aes.Padding = PaddingMode.None;
            Aes.KeySize = 128;
            Aes.Key = bKey;
            var cTransform = Aes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return logByte2(resultArray).Replace(":", "");
        }
        // <summary>
        // AES解密(无向量)
        // </summary>
        // <param name = "encryptedBytes" > 被加密的明文 </ param >
        // < param name="key">密钥</param>
        // <returns>明文</returns>
        public static string AESDecrypt(byte[] Data, byte[] Key, string aa)
        {
            try
            {
                byte[] encryptedBytes = Data;
                byte[] bKey = Key;
                Aes aes = Aes.Create();
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.KeySize = 128;
                aes.Key = bKey;
                var de = aes.CreateDecryptor();
                //解密数据
                var decodeByteData = de.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return logByte2(decodeByteData).Replace(":", "");
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }
        public static byte[] AESDecrypt(byte[] Data, byte[] Key)
        {
            try
            {
                byte[] encryptedBytes = Data;
                byte[] bKey = Key;
                Aes aes = Aes.Create();
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.KeySize = 128;
                aes.Key = bKey;
                var de = aes.CreateDecryptor();
                //解密数据
                var decodeByteData = de.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return decodeByteData;
            }
            catch (Exception ee)
            {
                return null;
            }
        }



        /// <summary>
        /// 16进制转明文
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HexStringToString(string hexString, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(hexString))
                return string.Empty;

            if (encoding == null)
                encoding = Encoding.ASCII;
            string[] byteitem = hexString.Trim().Split(';');
            List<byte> lstByte = new List<byte>();
            foreach (string item in byteitem)
                lstByte.Add(Convert.ToByte(item, 16));
            return encoding.GetString(lstByte.ToArray());

        }

        /// <summary>
        /// 16转10
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public static byte[] strings2bytes(String[] by)
        {
            byte[] s = new byte[by.Length]; //原byte
            int count = 0;
            foreach (string b in by)
            {
                s[count] = (byte)hex2ten(b);
                count++;
            }
            return s;
        }
        /// <summary>
        /// 切割
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string[] MySplit(string str, int count)
        {
            var list = new List<string>();
            int length = (int)Math.Ceiling((double)str.Length / count);

            for (int i = 0; i < length; i++)
            {
                int start = count * i;
                if (str.Length <= start)
                {
                    break;
                }
                if (str.Length < start + count)
                {
                    list.Add(str.Substring(start));
                }
                else
                {
                    list.Add(str.Substring(start, count));
                }
            }
            return list.ToArray();
        }
        //10转16
        public static string ten2Hex(byte b)
        {
            return Convert.ToString(b, 16);
        }

        //16转10
        public static int hex2ten(String str)
        {
            return Convert.ToInt32(str, 16);
        }

        public static int bytesToInt2(byte[] b)
        {
            return b[0] & 0xFF | (b[1] & 0xFF) << 8 | (b[2] & 0xFF) << 16 | (b[3] & 0xFF) << 24;
        }

        public static String logByte2(byte[] by)
        {
            String s = "";
            for (int i = 0; i < by.Length; i++)
            {
                byte b = by[i];
                String s1 = ten2Hex(b).ToLower();
                if (s1.Length == 1)
                {
                    s1 = "0" + s1;
                }
                s += s1 + ((i == by.Length - 1) ? "" : ":");
            }

            return s;
        }

        public static String bytesToStr(byte[] b)
        {

            String str = "";
            try
            {
                str = Encoding.UTF8.GetString(b);
            }
            catch (Exception e)
            {
                str=e.Message;
            }
            return str;
        }

        public static byte[] getKey(byte[] by)
        {
            byte[] s = new byte[16];
            int count = by.Length;
            for (int i = s.Length - 1; i >= 7; i--)
            {
                if (count > 0)
                {
                    s[i] = by[count-- - 1];
                }
                else
                {
                    s[i] = 0;
                }
            }
            s[0] = (byte)hex2ten("4B");
            s[1] = (byte)hex2ten("65");
            s[2] = (byte)hex2ten("48");
            s[3] = (byte)hex2ten("75");
            s[4] = (byte)hex2ten("61");
            s[5] = (byte)hex2ten("BF");
            s[6] = (byte)hex2ten("C6");
            return s;
        }

        /** 
         * 对金额的格式调整到分 
         * @param money 
         * @return 
         */
        public static string moneyFormat(string money)
        {//23->23.00  
            StringBuilder sb = new StringBuilder();
            if (money == null)
            {
                return "0.00";
            }
            int index = money.IndexOf(".");
            if (index == -1)
            {
                return money + ".00";
            }
            else
            {
                string s0 = money.Substring(0, index);//整数部分  
                string s1 = money.Substring(index + 1);//小数部分  
                if (s1.Count() == 1)
                {//小数点后一位  
                    s1 = s1 + "0";
                }
                else if (s1.Count() > 2)
                {//如果超过3位小数，截取2位就可以了  
                    s1 = s1.Substring(0, 2);
                }
                sb.Append(s0);
                sb.Append(".");
                sb.Append(s1);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(long timeStamp)
        {
            var date = new DateTime(1970, 1, 1).AddMilliseconds(timeStamp);
            return date;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
