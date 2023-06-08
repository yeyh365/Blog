using Blog.Application.Dto;
using Blog.Application.Services;
using Blog.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Blog.Application.Dto.Home;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region init
        private readonly IHomeService _HomeService;
        /// <summary>
        /// HomeController
        /// </summary>
        /// <param name="HomeService"></param>
        public HomeController(IHomeService HomeService)
        {
            this._HomeService = HomeService;
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
        [HttpGet, Route("GetRecommendInfo")]
        public async Task<PagableData<BannerDto>> GetRecommendInfo( CancellationToken cancellationToken)
         {
            return await _HomeService.GetRecommendInfo(cancellationToken);
        }
        #endregion
    }
}
