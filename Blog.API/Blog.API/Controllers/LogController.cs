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
    //[Authorize(Roles = "SuperAdmin")]
    public class LogController : ControllerBase
    {
        #region init
        private readonly ILogService _LogService;
        /// <summary>
        /// LogController
        /// </summary>
        /// <param name="LogService"></param>
        public LogController(ILogService LogService)
        {
            this._LogService = LogService;
        }
        #endregion
        #region Public
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetLogList")]
        public async Task<PagableData<LogDto>> GetLogList([FromQuery] LogSearch Search, CancellationToken cancellationToken)
        {
            return await _LogService.GetLogList(Search, cancellationToken);
        }
        #endregion
        #region Extend
        #endregion
    }
}
