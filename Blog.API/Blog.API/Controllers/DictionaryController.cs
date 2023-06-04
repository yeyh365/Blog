using Blog.Application.Dto;
using Blog.Application.Services;
using Blog.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        #region init
        private readonly IDictionaryService _DictionaryService;
        /// <summary>
        /// DictionaryController
        /// </summary>
        /// <param name="DictionaryService"></param>
        public DictionaryController(IDictionaryService DictionaryService)
        {
            this._DictionaryService = DictionaryService;
        }
        #endregion
        #region Public
        #endregion
        #region Extend
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDictionaryByType")]
        public async Task<ResultModel> GetDictionaryByType(string Type, CancellationToken cancellationToken)
         {
            return await _DictionaryService.GetDictionaryByType(Type, cancellationToken);
        }
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDictionaryList")]
        [Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
        public async Task<ResultModel> GetDictionaryList(string Type, CancellationToken cancellationToken)
        {
            return await _DictionaryService.GetDictionaryList(Type, cancellationToken);
        }
        #endregion
    }
}
