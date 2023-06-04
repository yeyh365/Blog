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
    /// UserController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
    public class UserController : ControllerBase
    {
        #region init
        private readonly IUserService _UserService;
        private readonly ILogService _LogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// UserController
        /// </summary>
        /// <param name="UserService"></param>
        public UserController(IUserService UserService, ILogService logService, IHttpContextAccessor _httpContextAccessor)
        {
            this._UserService = UserService;
            this._LogService = logService;
            this._httpContextAccessor = _httpContextAccessor;
        }
        #endregion
        #region Public
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUserList")]
        public async Task<PagableData<UserDto>> GetUserList([FromQuery] UserSearch Search, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),"查询用户列表");
            #endregion
            return await _UserService.GetUserList(Search, cancellationToken);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUserInfo/{Id}")]
        public async Task<ResultModel> GetUserInfo(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "查询用户详情");
            #endregion
            return await _UserService.GetUserInfo(Id, cancellationToken);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateUser")]
        public async Task<ResultModel> CreateUser(UserItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "创建用户");
            #endregion
            return await _UserService.CreateUser(Dto, cancellationToken);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="Dto">用户信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateUser")]
        public async Task<ResultModel> UpdateUser(UserItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "更新用户");
            #endregion
            return await _UserService.UpdateUser(Dto, cancellationToken);

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteUser/{Id}")]
        public async Task<ResultModel> DeleteUser(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "删除用户");
            #endregion
            return await _UserService.DeleteUser(Id, cancellationToken);
        }
        #endregion
        #region Extend
        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("FindLoginUser")]
        public async Task<ResultModel> FindLoginUser(CancellationToken cancellationToken)
        {
            return await _UserService.FindLoginUser(cancellationToken);
        }
        #endregion
    }
}
