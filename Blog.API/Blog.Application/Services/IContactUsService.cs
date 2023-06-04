using Microsoft.EntityFrameworkCore;
using Blog.Application.Dto;
using Blog.Application.Services.Impl;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    /// <summary>
    /// IContactUsService
    /// </summary>
    public interface IContactUsService
    {
        #region Public
        /// <summary>
        /// 获取联系我们列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<ContactUsDto>> GetContactUsList(ContactUsSearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 获取联系我们详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetContactUsInfo(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// 创建联系我们
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateContactUs(ContactUsItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 更新联系我们
        /// </summary>
        /// <param name="Dto">联系我们信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateContactUs(ContactUsItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除联系我们
        /// </summary>
        /// <param name="Id">联系我们id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteContactUs(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend

        #endregion
    }
}
