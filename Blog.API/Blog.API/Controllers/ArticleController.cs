using Blog.Application.Dto;
using Blog.Application.Services;
using Blog.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using Blog.Application.Services.Impl;
using Blog.Core.Enums;
using Blog.Core.Helper;

namespace Blog.WebAPI.Controllers
{
    /// <summary>
    /// ArticleController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
    public class ArticleController : ControllerBase
    {
        #region init
        private readonly IArticleService _ArticleService;
        private readonly ILogService _LogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// ArticleController
        /// </summary>
        /// <param name="ArticleService"></param>
        public ArticleController(IArticleService ArticleService, ILogService logService, IHttpContextAccessor _httpContextAccessor)
        {
            this._ArticleService = ArticleService;
            this._LogService = logService;
            this._httpContextAccessor = _httpContextAccessor;
        }
        #endregion
        #region Public
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetArticleList")]
        public async Task<PagableData<ArticleDto>> GetArticleList([FromQuery] ArticleSearch Search, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),"查询文章列表");
            #endregion
            return await _ArticleService.GetArticleList(Search, cancellationToken);
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetArticleInfo")]
        public async Task<ResultModel> GetArticleInfo( int Id, int UserId, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "查询文章详情");
            #endregion
            return await _ArticleService.GetArticleInfo(Id, UserId, cancellationToken);
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateArticle")]
        public async Task<ResultModel> CreateArticle(ArticleItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "创建文章");
            #endregion
            return await _ArticleService.CreateArticle(Dto, cancellationToken);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="Dto">文章信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateArticle")]
        public async Task<ResultModel> UpdateArticle(ArticleItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "更新文章");
            #endregion
            return await _ArticleService.UpdateArticle(Dto, cancellationToken);

        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="Id">文章id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteArticle/{Id}")]
        public async Task<ResultModel> DeleteArticle(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "删除文章");
            #endregion
            return await _ArticleService.DeleteArticle(Id, cancellationToken);
        }
        #endregion
        #region Extend
        [HttpGet, Route("GetArticleType")]
        public async Task<ResultModel> GetArticleType(CancellationToken cancellationToken)
        {
            return await _ArticleService.GetArticleType(cancellationToken);
        }
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken">传pathId</param>
        /// <returns></returns>
        [HttpGet, Route("GetArticleComment/{Id}")]
        public async Task<PagableData<CommentDto>> GetArticleComment(int Id, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "获取评论详情");
            #endregion
            return await _ArticleService.GetArticleComment(Id, cancellationToken);
        }
        /// <summary>
        /// 增加互动数据量
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateInteraction")]
        public async Task<ResultModel> CreateInteraction(InteractionItme Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "创建互动数据");
            #endregion
            return await _ArticleService.CreateInteraction(Dto, cancellationToken);
        }
        /// <summary>
        /// 更新互动数据量
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateInteraction")]
        public async Task<ResultModel> UpdateInteraction(InteractionItme Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "更新互动数据");
            #endregion
            return await _ArticleService.UpdateInteraction(Dto, cancellationToken);
        }
        /// <summary>
        /// 增加文章评论
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateArticleComment")]
        public async Task<ResultModel> CreateArticleComment(CommentItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "增加文章评论");
            #endregion
            return await _ArticleService.CreateArticleComment(Dto, cancellationToken);
        }
        /// <summary>
        /// 增加文章子级评论
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateChildComment")]
        public async Task<ResultModel> CreateChildComment(CommentItem Dto, CancellationToken cancellationToken)
        {
            #region Log日志
            _LogService.CreateLog(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), "增加文章子级评论");
            #endregion
            return await _ArticleService.CreateChildComment(Dto, cancellationToken);
        }
        #endregion
    }
}
