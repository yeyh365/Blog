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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Impl
{
        /// <summary>
        /// InteractionService
        /// </summary>
        public class InteractionService : ApplicationService, IInteractionService
        {
            #region init
            private readonly IRepository<Interaction> _InteractionRepository;
            private readonly IRepository<Dictionary> _DictionaryRepository;
            /// <summary>
            /// InteractionService
            /// </summary>
            /// <param name="context"></param>
            /// <param name="repositoryProvider"></param>
            /// <param name="mapper"></param>
            /// <param name="httpContext"></param>
            public InteractionService(
                IDBContext context,
                IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
            {
                this._InteractionRepository = this._repositoryProvider.Create<Interaction>(this._context);
                this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
            }
            #endregion
            #region Public
            public IQueryable<Interaction> GetSearch(InteractionSearch Search)
            {
                var Post = GetUserInfoByType("Post");
                var Account = GetUserInfoByType("Account");
                var Province = GetUserInfoByType("Province");
                var City = GetUserInfoByType("City");
                var County = GetUserInfoByType("County");

                var Query = this._InteractionRepository.GetAll();
                //if (!string.IsNullOrEmpty(Search.Title))
                //{
                //    Query = Query.Where(t => t.Title.Contains(Search.Title));
                //}
                //if (!string.IsNullOrEmpty(Search.Phone))
                //{
                //    Query = Query.Where(t => t.Phone.Equals(Search.Phone));
                //}
                return Query;
            }
            /// <summary>
            /// 获取联系我们列表
            /// </summary>
            /// <param name="Search"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<PagableData<InteractionDto>> GetInteractionList(InteractionSearch Search, CancellationToken cancellationToken)
            {
                var userId = GetUserInfoByType("Account");
                var Query = GetSearch(Search);
                var Total = await Query.CountAsync(cancellationToken);
                if (Search.Page > 0 && Search.Limit > 0)
                {
                    Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
                }
                List<Interaction> QueryList = Query.ToList();
                var ResultList = _mapper.Map<List<Interaction>, List<InteractionDto>>(QueryList);
                ResultList.ForEach(x =>
                {

                });
                if (QueryList.Count() <= 0)
                {
                    Total = 0;
                }
                return new PagableData<InteractionDto>
                {
                    Data = ResultList,
                    Total = Total
                };
            }

            /// <summary>
            /// 获取联系我们详情
            /// </summary>
            /// <param name="Id">学员ID</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<ResultModel> GetInteractionInfo(string TypeName, int ArticleId, int UserId, CancellationToken cancellationToken)
            {
                var result = new ResultModel();
                var DataModel = await _InteractionRepository.Get(x => x.UserId == UserId && x.TypeName== TypeName&&x.ArticleId== ArticleId).FirstOrDefaultAsync(cancellationToken);
                if (DataModel == null)
                {
                    result.Code = ResultCode.NotFound;
                    result.Message = "联系我们不存在";
                    return result;
                }
                InteractionDto DataInfo = _mapper.Map<Interaction, InteractionDto>(DataModel);
                result.Data = DataInfo;
                return result;
            }

            /// <summary>
            /// 创建联系我们
            /// </summary>
            /// <param name="Dto"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<ResultModel> CreateInteraction(InteractionItem Dto, CancellationToken cancellationToken)
            {
                ResultModel result = new ResultModel();
                var DataModel = _mapper.Map<Interaction>(Dto);
                using (var trans = this._context.BeginTrainsaction())
                {
                    _InteractionRepository.Insert(DataModel);
                    await this._context.SaveChangesAsync(cancellationToken);
                    trans.Commit();
                    result.Message = "创建成功！";
                    result.Data = DataModel;
                }
                return result;
            }

            /// <summary>
            /// 更新联系我们
            /// </summary>
            /// <param name="Dto">联系我们信息</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<ResultModel> UpdateInteraction(InteractionItem Dto, CancellationToken cancellationToken)
            {
                ResultModel result = new ResultModel();
                try
                {

                    var DataModel = _mapper.Map<Interaction>(Dto);
                    using (var trans = this._context.BeginTrainsaction())
                    {
                        _InteractionRepository.Update(DataModel);
                        await this._context.SaveChangesAsync(cancellationToken);
                        trans.Commit();
                        result.Message = "修改成功！";
                        result.Data = DataModel;
                    }
                }
                catch (Exception ee)
                {

                    var ss = 1;
                };

                return result;
            }

            /// <summary>
            /// 删除联系我们
            /// </summary>
            /// <param name="Id">Id</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<ResultModel> DeleteInteraction(int Id, CancellationToken cancellationToken)
            {
                ResultModel result = new ResultModel();
                _InteractionRepository.Delete(m => m.Id == Id);
                await _context.SaveChangesAsync(cancellationToken);
                result.Message = "删除成功！";
                return result;
            }
            #endregion
            #region Extend

            #endregion
        }
}
