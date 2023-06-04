

namespace Blog.Core.Helper
{
    using Blog.Core.Enums;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    public class ConfigHelper
    {
        public static T GetConfigurationSection<T>(IConfiguration configuration, string Name, ConfigType Type)
        {

            try
            {
                T ret = default(T);
                switch (Type)
                {
                    //string 类型
                    case ConfigType.String:
                        {
                            ret = (T)configuration.GetSection(Name);
                        }
                        break;
                    //实体类型
                    case ConfigType.Entity:
                        {

                            ret = (T)configuration.GetSection(Name);
                        }
                        break;
                    //列表
                    case ConfigType.List:
                        {
                            ret = (T)configuration.GetSection(Name);
                        }
                        break;
                    default:
                        {
                            ret = (T)configuration.GetSection(Name);
                        }
                        break;
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}