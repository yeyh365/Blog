using Blog.Application.Dto;
using Blog.Application.Services;
using Blog.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Entities;
using Blog.Core.Helper;
using Blog.Application.Services.Impl;
using System.Text.RegularExpressions;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly ITokenService _tokenService;
        private IHttpContextAccessor _httpContext;
        private ILogger<AuthController> _logger = null;

        public AuthController(
            IUserService UserService, ITokenService tokenService, IHttpContextAccessor httpContext, ILogger<AuthController> Logger)
        {
            this._UserService = UserService;
            this._tokenService = tokenService;
            _httpContext = httpContext;
            _logger = Logger;
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="User"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("Login")]
        public ResultModel Login([FromQuery]UserLogin Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            if (string.IsNullOrEmpty(Dto.Account) || string.IsNullOrEmpty(Dto.PassWord))
            {
                return new ResultModel { Code = Core.Enums.ResultCode.FailValidate, Message = "账号密码无效" };
            }
           // string PassWord= MD5Helper.Md5Method(Dto.PassWord);
            //Dto.PassWord = PassWord;
            var user = this._UserService.Login(Dto);
            if (user == null)
            { 
                result.Code = Core.Enums.ResultCode.NotFound;
                result.Message = "未获取到用户信息,请联系管理员.";
                return result;
            }
            var Token = _tokenService.GetToken(user);
            result.Code = Core.Enums.ResultCode.Success;
            result.Data = Token;
            return result;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="User"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("RegisterUser")]
        public async Task<ResultModel> RegisterUser(UserItem Dto, CancellationToken cancellationToken)
        {

            return await _UserService.RegisterUser(Dto, cancellationToken);
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="User"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("SendCode")]
        public ResultModel SendCode(UserLogin Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            if (string.IsNullOrEmpty(Dto.Account))
            {
                return new ResultModel { Code = Core.Enums.ResultCode.FailValidate, Message = "账号密码无效" };
            }
            string patten1 = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            var a = Regex.IsMatch(Dto.Email, patten1);
            if (!string.IsNullOrEmpty(Dto.Email)&&!Regex.IsMatch(Dto.Email, patten1))
            {
                return new ResultModel { Code = Core.Enums.ResultCode.FailValidate, Message = "邮箱无效" };
            }
            //string PassWord = MD5Helper.Md5Method(Dto.PassWord);
            //Dto.PassWord = PassWord;

            var user = this._UserService.SendCode(Dto);
            if (user == null)
            {
                result.Code = Core.Enums.ResultCode.NotFound;
                result.Message = "未获取到用户信息,请联系管理员.";
                return result;
            }
            result.Code = Core.Enums.ResultCode.Success;
            result.Data = user;
            return result;
        }
    }
}
