using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    /// <summary>
    /// WeChatConfig配置
    /// </summary>
    public class WeChatConfig
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; } = "";
        /// <summary>
        /// Secret
        /// </summary>

        public string Secret { get; set; } = "";
        /// <summary>
        /// BaseUrl
        /// </summary>

        public string BaseUrl { get; set; } = "";

        /// <summary>
        /// Account
        /// </summary>

        public string Account { get; set; } = "";

        /// <summary>
        /// Key
        /// </summary>

        public string Key { get; set; } = "";
        /// <summary>
        /// GaudUrl
        /// </summary>

        public string GaudUrl { get; set; } = "";

        /// <summary>
        /// GaudConvertUrl
        /// </summary>

        public string GaudConvertUrl { get; set; } = "";
        
        /// <summary>
        /// GaudKey
        /// </summary>

        public string GaudKey { get; set; } = "";

    }
    public class WeChatConfigHelper
    {
        public static WeChatConfig GetConfig(IConfiguration configuration)
        {
            WeChatConfig config = new WeChatConfig();
            var section = configuration.GetSection("WeChatConfig");
            if (section == null) throw new KeyNotFoundException("WeChatConfig, key:WeChatConfig not found");
            config.AppId = section["AppId"];
            config.Secret = section["Secret"];
            config.BaseUrl = section["BaseUrl"];
            config.Account = section["Account"];
            config.Key = section["Key"];
            config.GaudUrl = section["GaudUrl"];
            config.GaudKey = section["GaudKey"];
            config.GaudConvertUrl = section["GaudConvertUrl"];
            
            return config;
        }
    }
}
