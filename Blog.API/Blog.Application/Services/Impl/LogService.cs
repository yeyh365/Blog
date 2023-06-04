
using AutoMapper;
using Blog.Application.Dto;
using Blog.Core.Enums;
using Blog.Core.Model;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrainingPlaning.Core.Helper;
using Blog.Core.Helper;

namespace Blog.Application.Services.Impl
{
    /// <summary>
    /// LogService
    /// </summary>
    public class LogService : ApplicationService, ILogService
    {
        #region init
        private readonly IRepository<Log> _LogRepository;
        /// <summary>
        /// LogService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public LogService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._LogRepository = this._repositoryProvider.Create<Log>(this._context);
        }
        #endregion
        #region Public
        /// <summary>
        /// 获取工作日志列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagableData<LogDto>> GetLogList(LogSearch Search, CancellationToken cancellationToken)
        {
            var userId = GetUserInfoByType("Account");
            var Query = this._LogRepository.GetAll();
            if (!string.IsNullOrEmpty(Search.Account))
            {
                Query = Query.Where(t => t.Name.Contains(Search.Account));
            }
            if (Search.StartDate != null)
            {
                Query = Query.Where(t => t.Created >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                Search.EndDate = Convert.ToDateTime(Search.EndDate).AddDays(1);
                Query = Query.Where(t => t.Created <= Search.EndDate);
            }
            if (!string.IsNullOrEmpty(Search.OptionType))
            {
                Query = Query.Where(t => t.Type.Contains(Search.OptionType));
            }
            var Total = await Query.CountAsync(cancellationToken);
            if (Search.Page > 0 && Search.Limit > 0)
            {
                Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            }
            List<Log> QueryList = Query.ToList();
            var ResultList = _mapper.Map<List<Log>, List<LogDto>>(QueryList);
            if (QueryList.Count() <= 0)
            {
                Total = 0;
            }
            return new PagableData<LogDto>
            {
                Data = ResultList,
                Total = Total
            };
        }

        /// <summary>
        /// 获取工作日志详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetLogInfo(int Id, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            Log DataModel = await _LogRepository.Get(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "工作日志不存在";
                return result;
            }
            LogDto DataInfo = _mapper.Map<Log, LogDto>(DataModel);
            result.Data = DataInfo;
            return result;
        }

        /// <summary>
        /// 创建工作日志
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ResultModel CreateLog(string IP, string Type="", string Description ="")
        {
            LogDto Dto = new LogDto() {
                Name = GetUserInfoByType("Name"),
                Account = GetUserInfoByType("Account"),
                Type = Type,
                IP= IP,
                Description = Description
            };

            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<Log>(Dto);
            var CheckModel = this._LogRepository.Get(x => x.Name == DataModel.Name).FirstOrDefault();
            _LogRepository.Insert(DataModel);
            this._context.SaveChanges();

            result.Message = "创建成功！";
            result.Data = DataModel;
            return result;
        }

        /// <summary>
        /// 更新工作日志
        /// </summary>
        /// <param name="Dto">工作日志信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> UpdateLog(LogDto Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            try
            {

                var DataModel = _mapper.Map<Log>(Dto);
                Log CheckModel = this._LogRepository.Get(x => x.Name == DataModel.Name && x.Id != DataModel.Id).FirstOrDefault();
                if (CheckModel != null)
                {
                    result.Code = ResultCode.NotFound;
                    result.Message = "工作日志已存在";
                    return result;
                }
                _LogRepository.Update(DataModel);
                await this._context.SaveChangesAsync(cancellationToken);
                result.Message = "修改成功！";
                result.Data = DataModel;
            }
            catch (Exception ee)
            {

                var ss = 1;
            };

            return result;
        }

        /// <summary>
        /// 删除工作日志
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> DeleteLog(int Id, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            _LogRepository.Delete(m => m.Id == Id);
            await _context.SaveChangesAsync(cancellationToken);
            result.Message = "删除成功！";
            return result;
        }
        #endregion
        #region Extend
        #endregion
    }
}
