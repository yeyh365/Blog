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
    /// IArticleService
    /// </summary>
    public interface IArticleService
    {
        #region Public
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<ArticleDto>> GetArticleList(ArticleSearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetArticleInfo(int Id, int UserId, CancellationToken cancellationToken);
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<CommentDto>> GetArticleComment(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateArticle(ArticleItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="Dto">文章信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateArticle(ArticleItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="Id">文章id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteArticle(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend
        Task<ResultModel> GetArticleType(CancellationToken cancellationToken);

        Task<ResultModel> CreateInteraction(InteractionItem Itme, CancellationToken cancellationToken);
        Task<ResultModel> UpdateInteraction(InteractionItem Itme, CancellationToken cancellationToken);
        /// <summary>
        /// 增加一个文章评论
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateArticleComment(CommentItem Dto, CancellationToken cancellationToken);
        /// <summary>
        /// 增加一个子级评论
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateChildComment(CommentItem Dto, CancellationToken cancellationToken);
        #endregion
    }
}
