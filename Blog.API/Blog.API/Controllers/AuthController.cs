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

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _AdministratorService;
        private readonly ITokenService _tokenService;
        private IHttpContextAccessor _httpContext;
        private ILogger<AuthController> _logger = null;

        public AuthController(
            IUserService AdministratorService, ITokenService tokenService, IHttpContextAccessor httpContext, ILogger<AuthController> Logger)
        {
            this._AdministratorService = AdministratorService;
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
        [HttpPost, Route("Login")]
        public ResultModel Login(UserLogin Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            if (string.IsNullOrEmpty(Dto.Account) || string.IsNullOrEmpty(Dto.PassWord))
            {
                return new ResultModel { Code = Core.Enums.ResultCode.FailValidate, Message = "账号密码无效" };
            }
            string PassWord= MD5Helper.Md5Method(Dto.PassWord);
            Dto.PassWord = PassWord;
            var user = this._AdministratorService.Login(Dto);
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
    }
}
