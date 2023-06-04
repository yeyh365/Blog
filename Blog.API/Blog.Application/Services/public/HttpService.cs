using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Blog.Application.Dto;
using Blog.Core.Enums;
using Blog.Core.Model;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class HttpService : ApplicationService, IHttpService
    {
        #region init

        private readonly HttpClient _Client;
        /// <summary>
        /// DictionaryService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public HttpService(
            IHttpClientFactory clientFactory
           , IConfiguration Configuration) : base(clientFactory, Configuration)
        {
            _Client = clientFactory.CreateClient();
        }
        #endregion
        #region Public
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
            var result = await _Client.GetStringAsync(url);
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
        public async Task<string> PostAsync(string url, Dictionary<string, string> Paras, Dictionary<string, string> Header = null, params object[] keyIds)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            //参数
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
            content.Headers.ContentType=System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            //Hear
            if (Header != null)
            {
                foreach (var keyvalues in Header)
                {
                    if(keyvalues.Key== "Content-Type")
                    {
                        content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    }
                    else
                    {
                        content.Headers.Add(keyvalues.Key, keyvalues.Value);
                    }
                } 
            }
            HttpResponseMessage response = await _Client.PostAsync(url, content);
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
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="signParas"></param>
        /// <param name="notSignParas"></param>
        /// <param name="keyIds"></param>
        /// <returns></returns>
        public async Task<string> PostAsyncStringContent(string url, string Paras, Dictionary<string, string> Header = null, params object[] keyIds)
        {
            //参数
            if (keyIds != null && keyIds.Length > 0)
            {
                url = string.Format(url, keyIds);
            }


            var content = new StringContent(Paras, Encoding.UTF8, "application/json");
            //Hear
            if (Header != null)
            {
                foreach (var keyvalues in Header)
                {
                    if (keyvalues.Key == "Content-Type")
                    {
                        content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    }
                    else
                    {
                        content.Headers.Add(keyvalues.Key, keyvalues.Value);
                    }
                }
            }
            HttpResponseMessage response = await _Client.PostAsync(url, content);
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

        //private async Task<string> GetStringAsync(string url)
        //{
        //    Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //    return await _Client.GetStringAsync(url);
        //}

        //private async Task<string> PostAsync(string url, Dictionary<string, string> keyValues)
        //{
        //    var content = new FormUrlEncodedContent(keyValues);

        //    HttpResponseMessage response = await _Client.PostAsync(url, content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var t = await response.Content.ReadAsStringAsync();
        //        return t;
        //    }

        //    return "";
        //}
        #endregion
        #region Extend
        #endregion
    }
}
