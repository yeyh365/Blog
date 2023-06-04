
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
using System.ComponentModel;
using Org.BouncyCastle.Bcpg;
using Blog.Core.Helper;
using System.Security.Claims;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.POIFS.Properties;

namespace Blog.Application.Services.Impl
{
    /// <summary>
    /// ContactUsService
    /// </summary>
    public class ContactUsService : ApplicationService, IContactUsService
    {
        #region init
        private readonly IRepository<ContactUs> _ContactUsRepository;
        private readonly IRepository<Dictionary> _DictionaryRepository;
        /// <summary>
        /// ContactUsService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public ContactUsService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._ContactUsRepository = this._repositoryProvider.Create<ContactUs>(this._context);
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
        }
        #endregion
        #region Public
        public IQueryable<ContactUs> GetSearch(ContactUsSearch Search)
        {
            var Post = GetUserInfoByType("Post");
            var Account = GetUserInfoByType("Account");
            var Province = GetUserInfoByType("Province");
            var City = GetUserInfoByType("City");
            var County = GetUserInfoByType("County");

            var Query = this._ContactUsRepository.GetAll();
            if (!string.IsNullOrEmpty(Search.Title))
            {
                Query = Query.Where(t => t.Title.Contains(Search.Title));
            }
            if (!string.IsNullOrEmpty(Search.Phone))
            {
                Query = Query.Where(t => t.Phone.Equals(Search.Phone));
            }
            return Query;
        }
        /// <summary>
        /// 获取联系我们列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagableData<ContactUsDto>> GetContactUsList(ContactUsSearch Search, CancellationToken cancellationToken)
        {
            var userId = GetUserInfoByType("Account");
            var Query = GetSearch(Search);
            var Total = await Query.CountAsync(cancellationToken);
            if (Search.Page > 0 && Search.Limit > 0)
            {
                Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            }
            List<ContactUs> QueryList = Query.ToList();
            var ResultList = _mapper.Map<List<ContactUs>, List<ContactUsDto>>(QueryList);
            ResultList.ForEach(x =>
            {
               
            });
            if (QueryList.Count() <= 0)
            {
                Total = 0;
            }
            return new PagableData<ContactUsDto>
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
        public async Task<ResultModel> GetContactUsInfo(int Id, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            var DataModel = await _ContactUsRepository.Get(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "联系我们不存在";
                return result;
            }
            ContactUsDto DataInfo = _mapper.Map<ContactUs, ContactUsDto>(DataModel);
            result.Data = DataInfo;
            return result;
        }

        /// <summary>
        /// 创建联系我们
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> CreateContactUs(ContactUsItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<ContactUs>(Dto);
            using (var trans = this._context.BeginTrainsaction())
            {
                _ContactUsRepository.Insert(DataModel);
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
        public async Task<ResultModel> UpdateContactUs(ContactUsItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            try
            {

                var DataModel = _mapper.Map<ContactUs>(Dto);
                using (var trans = this._context.BeginTrainsaction())
                {
                    _ContactUsRepository.Update(DataModel);
                    await this._context.SaveChangesAsync(cancellationToken);
                    trans.Commit();
                    result.Message = "修改成功！";
                    result.Data = DataModel;
                }
            }
            catch (Exception ee) 
            {

                var ss = 1; };

            return result;
        }

        /// <summary>
        /// 删除联系我们
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> DeleteContactUs(int Id, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            _ContactUsRepository.Delete(m => m.Id == Id);
            await _context.SaveChangesAsync(cancellationToken);
            result.Message = "删除成功！";
            return result;
        }
        #endregion
        #region Extend

        #endregion
    }
}
