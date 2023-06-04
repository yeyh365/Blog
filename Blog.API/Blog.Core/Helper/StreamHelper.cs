using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Helper
{
    public class StreamHelper
    {        /// <summary>
             /// Stream To byte[]
             /// </summary>
             /// <param name="fs"></param>
             /// <returns></returns>
        public static byte[] ConvertStreamToByte(Stream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Position = 0;
            fs.Read(buffer, 0, (int)fs.Length);
            fs.Seek(0, SeekOrigin.Begin);

            return buffer;
        }

        /// <summary>
        /// 将Stream流保存到Stream流
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static bool CopyStreamToFile(Stream input, Stream output)
        {
            try
            {
                int Length = 256;
                Byte[] buffer = new Byte[Length];
                input.Seek(0, SeekOrigin.Begin);
                int bytesRead = input.Read(buffer, 0, Length);

                while (bytesRead > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                    bytesRead = input.Read(buffer, 0, Length);
                }
                output.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将Stream流保存到文件
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public bool CopyStreamToFile(Stream input, string outputfile)
        {
            using (Stream file = File.OpenWrite(outputfile))
            {
                try
                {
                    int Length = 256;
                    Byte[] buffer = new Byte[Length];
                    input.Seek(0, SeekOrigin.Begin);
                    int bytesRead = input.Read(buffer, 0, Length);

                    while (bytesRead > 0)
                    {
                        file.Write(buffer, 0, bytesRead);
                        bytesRead = input.Read(buffer, 0, Length);
                    }
                    file.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 将文件转成byte[]
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ConvertFileToByte(string path)
        {
            System.IO.FileStream fileStream = null;
            byte[] bytes = null;

            try
            {
                fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                bytes = new byte[(int)fileStream.Length];
                fileStream.Read(bytes, 0, (int)fileStream.Length);
            }
            catch
            {

            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return bytes;
        }

        /// <summary>
        /// 将byte[]转成文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ConvertByteToFile(byte[] buffer, string file)
        {
            try
            {
                using (Stream sm = File.OpenWrite(file))
                {
                    try
                    {
                        while (buffer.Length > 0)
                        {
                            sm.Write(buffer, 0, buffer.Length);
                        }
                        sm.Close();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        public MemoryStream BytesToMemoryStream(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
