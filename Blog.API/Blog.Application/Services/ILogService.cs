using Blog.Application.Dto;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    /// <summary>
    /// ILogService
    /// </summary>
    public interface ILogService
    {
        #region Public
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<LogDto>> GetLogList(LogSearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 获取日志详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetLogInfo(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ResultModel CreateLog(string IP, string Type = "", string Description = "");

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <param name="Dto">日志信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateLog(LogDto Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="Id">日志id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteLog(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend
        #endregion
    }
}
