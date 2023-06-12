using Blog.Application.Dto;
using Blog.Application.Services;
using Blog.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using Blog.Application.Services.Impl;
using Blog.Core.Enums;
using Blog.Core.Helper;
using System;

namespace Blog.WebAPI.Controllers
{
    /// <summary>
    /// InteractionController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
    public class InteractionController : ControllerBase
    {
        #region init
        private readonly IInteractionService _InteractionService;
        private readonly ILogService _LogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// InteractionController
        /// </summary>
        /// <param name="InteractionService"></param>
        public InteractionController(IInteractionService InteractionService, ILogService logService, IHttpContextAccessor _httpContextAccessor)
        {
            this._InteractionService = InteractionService;
            this._LogService = logService;
            this._httpContextAccessor = _httpContextAccessor;
        }
        #endregion
        #region Public
        /// <summary>
        /// 获取联系我们列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetInteractionList")]
        public async Task<PagableData<InteractionDto>> GetInteractionList([FromQuery] InteractionSearch Search, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),"查询联系我们列表");
            #endregion
            return await _InteractionService.GetInteractionList(Search, cancellationToken);
        }

        /// <summary>
        /// 获取联系我们详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetInteractionInfo")]
        public async Task<ResultModel> GetInteractionInfo(string TypeName, int ArticleId, int UserId, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "查询联系我们详情");
            #endregion
            return await _InteractionService.GetInteractionInfo(TypeName, ArticleId, UserId, cancellationToken);
        }

        /// <summary>
        /// 创建联系我们
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateInteraction")]
        public async Task<ResultModel> CreateInteraction(InteractionItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "创建联系我们");
            #endregion
            return await _InteractionService.CreateInteraction(Dto, cancellationToken);
        }

        /// <summary>
        /// 更新联系我们
        /// </summary>
        /// <param name="Dto">联系我们信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateInteraction")]
        public async Task<ResultModel> UpdateInteraction(InteractionItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "更新联系我们");
            #endregion
            return await _InteractionService.UpdateInteraction(Dto, cancellationToken);

        }

        /// <summary>
        /// 删除联系我们
        /// </summary>
        /// <param name="Id">联系我们id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteInteraction/{Id}")]
        public async Task<ResultModel> DeleteInteraction(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "删除联系我们");
            #endregion
            return await _InteractionService.DeleteInteraction(Id, cancellationToken);
        }
        #endregion
        #region Extend

        #endregion
    }
}
