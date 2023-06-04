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
    /// IUserService
    /// </summary>
    public interface IUserService
    {
        #region Public
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<UserDto>> GetUserList(UserSearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 获取管理员详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetUserInfo(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateUser(UserItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 更新管理员
        /// </summary>
        /// <param name="Dto">管理员信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateUser(UserItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">管理员id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteUser(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       UserDto Login(UserLogin Dto);
        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        Task<ResultModel> FindLoginUser(CancellationToken cancellationToken);
        #endregion
    }
}
