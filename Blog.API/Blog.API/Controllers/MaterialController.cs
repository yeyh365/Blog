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
using System.ComponentModel.Design;

namespace Blog.WebAPI.Controllers
{
    /// <summary>
    /// MaterialController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
    public class MaterialController : ControllerBase
    {
        #region init
        private readonly IMaterialService _MaterialService;
        private readonly ILogService _LogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// MaterialController
        /// </summary>
        /// <param name="MaterialService"></param>
        public MaterialController(IMaterialService MaterialService, ILogService logService, IHttpContextAccessor _httpContextAccessor)
        {
            this._MaterialService = MaterialService;
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
        [HttpGet, Route("GetMaterialList")]
        public async Task<PagableData<MaterialDto>> GetMaterialList([FromQuery] MaterialSearch Search, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),"查询联系我们列表");
            #endregion
            return await _MaterialService.GetMaterialList(Search, cancellationToken);
        }

        /// <summary>
        /// 获取联系我们详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMaterialInfo")]
        public async Task<ResultModel> GetMaterialInfo([FromQuery] MaterialSearch Search, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "查询联系我们详情");
            #endregion
            return await _MaterialService.GetMaterialInfo(Search, cancellationToken);
        }

        /// <summary>
        /// 创建联系我们
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateMaterial")]
        public async Task<ResultModel> CreateMaterial(MaterialItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "创建联系我们");
            #endregion
            return await _MaterialService.CreateMaterial(Dto, cancellationToken);
        }

        /// <summary>
        /// 更新联系我们
        /// </summary>
        /// <param name="Dto">联系我们信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateMaterial")]
        public async Task<ResultModel> UpdateMaterial(MaterialItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "更新联系我们");
            #endregion
            return await _MaterialService.UpdateMaterial(Dto, cancellationToken);

        }

        /// <summary>
        /// 删除联系我们
        /// </summary>
        /// <param name="Id">联系我们id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteMaterial/{Id}")]
        public async Task<ResultModel> DeleteMaterial(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "删除联系我们");
            #endregion
            return await _MaterialService.DeleteMaterial(Id, cancellationToken);
        }
        #endregion
        #region Extend
        [HttpGet, Route("GetMaterialType")]
        public async Task<ResultModel> GetMaterialType(CancellationToken cancellationToken)
        {
            return await _MaterialService.GetMaterialType(cancellationToken);
        }
        #endregion
    }
}
