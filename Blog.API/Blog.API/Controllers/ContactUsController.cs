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

namespace Blog.WebAPI.Controllers
{
    /// <summary>
    /// ContactUsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
    public class ContactUsController : ControllerBase
    {
        #region init
        private readonly IContactUsService _ContactUsService;
        private readonly ILogService _LogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// ContactUsController
        /// </summary>
        /// <param name="ContactUsService"></param>
        public ContactUsController(IContactUsService ContactUsService, ILogService logService, IHttpContextAccessor _httpContextAccessor)
        {
            this._ContactUsService = ContactUsService;
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
        [HttpGet, Route("GetContactUsList")]
        public async Task<PagableData<ContactUsDto>> GetContactUsList([FromQuery] ContactUsSearch Search, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),"查询联系我们列表");
            #endregion
            return await _ContactUsService.GetContactUsList(Search, cancellationToken);
        }

        /// <summary>
        /// 获取联系我们详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetContactUsInfo/{Id}")]
        public async Task<ResultModel> GetContactUsInfo(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "查询联系我们详情");
            #endregion
            return await _ContactUsService.GetContactUsInfo(Id, cancellationToken);
        }

        /// <summary>
        /// 创建联系我们
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateContactUs")]
        public async Task<ResultModel> CreateContactUs(ContactUsItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "创建联系我们");
            #endregion
            return await _ContactUsService.CreateContactUs(Dto, cancellationToken);
        }

        /// <summary>
        /// 更新联系我们
        /// </summary>
        /// <param name="Dto">联系我们信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateContactUs")]
        public async Task<ResultModel> UpdateContactUs(ContactUsItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "更新联系我们");
            #endregion
            return await _ContactUsService.UpdateContactUs(Dto, cancellationToken);

        }

        /// <summary>
        /// 删除联系我们
        /// </summary>
        /// <param name="Id">联系我们id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteContactUs/{Id}")]
        public async Task<ResultModel> DeleteContactUs(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "删除联系我们");
            #endregion
            return await _ContactUsService.DeleteContactUs(Id, cancellationToken);
        }
        #endregion
        #region Extend

        #endregion
    }
}
