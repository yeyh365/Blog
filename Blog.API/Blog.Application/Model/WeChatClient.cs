using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Blog.Application.Dto;
using Blog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Application.Model
{
    public class WeChatClient
    {
        private readonly HttpClient _client;
        private readonly WeChatConfig _config;
        private static IMemoryCache _cache;
        /// <summary>
        /// WeChatClient
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="configuration"></param>
        public WeChatClient(IHttpClientFactory clientFactory, IConfiguration configuration, IMemoryCache memoryCache)
        {
            var client = clientFactory.CreateClient();
            _config = (WeChatConfig)configuration.GetSection("WeChatConfig");
            client.BaseAddress = new Uri(_config.BaseUrl);
            _client = client;
            _cache = memoryCache;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="signParas"></param>
        /// <param name="notSignParas"></param>
        /// <param name="keyIds"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url, Dictionary<string, string> Paras, params object[] keyIds)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (Paras != null)
            {
                foreach (var keyvalues in Paras)
                {
                    dict.Add(keyvalues.Key, keyvalues.Value);
                }
            }
            url = string.Format(url, keyIds);
            url = QueryHelpers.AddQueryString(url, dict);
            var result = await _client.GetStringAsync(url);
            return result;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="signParas"></param>
        /// <param name="notSignParas"></param>
        /// <param name="keyIds"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string url, Dictionary<string, string> Paras, params object[] keyIds)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (Paras != null)
            {
                foreach (var keyvalues in Paras)
                {
                    dict.Add(keyvalues.Key, keyvalues.Value);
                }
            }
            if (keyIds != null && keyIds.Length > 0)
            {
                url = string.Format(url, keyIds);
            }
            var content = new FormUrlEncodedContent(dict);
            HttpResponseMessage response = await _client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Unicode转码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string DeCode(string str)
        {
            var regex = new Regex(@"\\u(\w{4})");

            string result = regex.Replace(str, m =>
            {
                string hexStr = m.Groups[1].Value;
                string charStr = ((char)int.Parse(hexStr, System.Globalization.NumberStyles.HexNumber)).ToString();
                return charStr;
            });

            return result;
        }

        private async Task<string> GetStringAsync(string url)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return await _client.GetStringAsync(url);
        }

        private async Task<string> PostAsync(string url, Dictionary<string, string> keyValues)
        {
            var content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await _client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var t = await response.Content.ReadAsStringAsync();
                return t;
            }

            return "";
        }

        #region 企业微信操作
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken(string appid, string secret)
        {
            string AccessToken = "";

            _cache.TryGetValue("AccessToken", out AccessToken);
            //if(string.IsNullOrEmpty(AccessToken))
            //{
            //    AccessToken = GetTheAccessToken(appid, secret);
            //    CacheHelper.Add("AccessToken", AccessToken);
            //    CacheHelper.Add("Date", DateTime.Now);
            //}
            //else
            //{
            //    DateTime CreateTime = CacheHelper.Get<DateTime>("Date");
            //    DateTime NowTime = DateTime.Now;
            //    int c = (NowTime - CreateTime).Days * 1440 + (NowTime - CreateTime).Hours * 60 + (NowTime - CreateTime).Minutes;
            //    if (c > 110)
            //    {
            //        AccessToken = Wechelper.GetTheAccessToken(corpid, corpsecret);
            //    }
            //    else
            //    {
            //        AccessToken = CacheHelper.Get<string>("AccessToken");
            //    }
            //}
            return AccessToken;
        }
        public static string GetTheAccessToken(string appid, string secret)
        {
            string AccessToken = "";
            var result = new Dictionary<string, double>();
            string geocoderUrl = string.Format("https://api.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", appid, secret);
            //string value = GetAsync(geocoderUrl, new Dictionary<string, string>(), new object());
            //var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value);
            //if (json != null && json.access_token != null)
            //{
            //    AccessToken = json.access_token;
            //}
            return AccessToken;
        }
        /// <summary>
        /// 获取Ticket
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        //public static string GetTheTicket(string Token)
        //{
        //    string Ticket = "";
        //    if (!CacheHelper.Exsits("Ticket"))
        //    {
        //        Ticket = Wechelper.GetTheTicket(Token);
        //        CacheHelper.Add("Ticket", Ticket);
        //        CacheHelper.Add("TicketDate", DateTime.Now);
        //    }
        //    else
        //    {
        //        DateTime CreateTime = CacheHelper.Get<DateTime>("TicketDate");
        //        DateTime NowTime = DateTime.Now;
        //        int c = (NowTime - CreateTime).Days * 1440 + (NowTime - CreateTime).Hours * 60 + (NowTime - CreateTime).Minutes;
        //        if (c > 110)
        //        {
        //            Ticket = Wechelper.GetTheTicket(Token);
        //        }
        //        else
        //        {
        //            Ticket = CacheHelper.Get<string>("Ticket");
        //        }
        //    }
        //    return Ticket;
        //}
        #endregion
    }
}
