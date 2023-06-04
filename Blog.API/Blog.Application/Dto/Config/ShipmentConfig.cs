using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace Blog.Application.Dto.Config
{
    public class ShipmentConfig
    {
        /// <summary>
        /// BaseUrl
        /// </summary>

        public string BaseUrl { get; set; } = "";

        /// <summary>
        /// appId
        /// </summary>

        public string appId { get; set; } = "";

        /// <summary>
        /// appPrivateKey
        /// </summary>

        public string appPrivateKey { get; set; } = "";
    }
    public class ShipmentConfigHelper
    {
        public static ShipmentConfig GetConfig(IConfiguration configuration)
        {
            ShipmentConfig config = new ShipmentConfig();
            var section = configuration.GetSection("ShipmentConfig");
            if (section == null) throw new KeyNotFoundException("ShipmentConfig, key:ShipmentConfig not found");
            config.appId = section["appId"];
            config.appPrivateKey = section["appPrivateKey"];
            config.BaseUrl = section["BaseUrl"];
           
            return config;
        }
    }

    public class ShipmentOnLine
    {
        /// <summary>
        /// is_online
        /// </summary>

        public string is_online { get; set; } = "";

        /// <summary>
        /// address
        /// </summary>

        public string address { get; set; } = "";

        /// <summary>
        /// Sim_account
        /// </summary>

        public string Sim_account { get; set; } = "";
    }

    public class ShipmentProduct
    {
        /// <summary>
        /// code
        /// </summary>

        public string code { get; set; } = "";

        /// <summary>
        /// msg
        /// </summary>

        public string msg { get; set; } = "";

        /// <summary>
        /// goods
        /// </summary>

        public List<good> goods { get; set; }
    }
    public class good
    {
        /// <summary>
        /// pic_url
        /// </summary>

        public string pic_url { get; set; } = "";

        /// <summary>
        /// name
        /// </summary>

        public string name { get; set; } = "";

        /// <summary>
        /// id
        /// </summary>

        public string id { get; set; } = "";

        /// <summary>
        /// count
        /// </summary>

        public string count { get; set; } = "";

        /// <summary>
        /// goodCode
        /// </summary>

        public string goodCode { get; set; } = "";

        /// <summary>
        /// unit_price
        /// </summary>

        public string unit_price { get; set; } = "";
    }
    public class ShipmentGood
    {
        /// <summary>
        /// code
        /// </summary>

        public string code { get; set; } = "";

        /// <summary>
        /// msg
        /// </summary>

        public string msg { get; set; } = "";

        /// <summary>
        /// goods
        /// </summary>

        public string goodCode { get; set; } = "";
    }
    public class ShipmentGoodCount
    {
        /// <summary>
        /// code
        /// </summary>

        public string id { get; set; } = "";

        /// <summary>
        /// msg
        /// </summary>

        public string count { get; set; } = "";
    }
}
