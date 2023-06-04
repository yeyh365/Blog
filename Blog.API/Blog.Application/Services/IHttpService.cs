using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public interface IHttpService
    {
        #region Public
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="signParas"></param>
        /// <param name="notSignParas"></param>
        /// <param name="keyIds"></param>
        /// <returns></returns>
        Task<string> GetAsync(string url, Dictionary<string, string> Paras, params object[] keyIds);

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="signParas"></param>
        /// <param name="notSignParas"></param>
        /// <param name="keyIds"></param>
        /// <returns></returns>
        Task<string> PostAsync(string url, Dictionary<string, string> Paras, Dictionary<string, string> Header = null, params object[] keyIds);
        Task<string> PostAsyncStringContent(string url, string Paras, Dictionary<string, string> Header = null, params object[] keyIds);
        #endregion
        #region Extend
        #endregion
    }
}
