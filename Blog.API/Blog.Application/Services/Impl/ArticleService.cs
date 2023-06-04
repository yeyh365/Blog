
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
using NPOI.SS.Formula.Functions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;

namespace Blog.Application.Services.Impl
{
    /// <summary>
    /// ArticleService
    /// </summary>
    public class ArticleService : ApplicationService, IArticleService
    {
        #region init
        private readonly IRepository<Article> _ArticleRepository;
        private readonly IRepository<Dictionary> _DictionaryRepository;
        private readonly IRepository<Keywords> _KeywordsRepository;
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<MaterialArticleKeywords> _ArticleKeywordsRepository;
        private readonly IRepository<Interaction> _InteractionRepository;
        private readonly IRepository<Comment> _CommentRepository;

        /// <summary>
        /// ArticleService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public ArticleService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._ArticleRepository = this._repositoryProvider.Create<Article>(this._context);
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
            this._KeywordsRepository = this._repositoryProvider.Create<Keywords>(this._context);
            this._UserRepository = this._repositoryProvider.Create<User>(this._context);
            this._ArticleKeywordsRepository = this._repositoryProvider.Create<MaterialArticleKeywords>(this._context);
            this._InteractionRepository = this._repositoryProvider.Create<Interaction>(this._context);
            this._CommentRepository = this._repositoryProvider.Create<Comment>(this._context);




        }
        #endregion
        #region Public
        public List<ArticleDto> GetSearch(ArticleSearch Search)
        {

            var Query = this._ArticleRepository.GetAll();
            //if (Search.Id > 0)
            //{
            //    Query = Query.Where(t => t.Id == Search.Id);
            //}

            if (!string.IsNullOrEmpty(Search.ArticleTitle))
            {
                Query = Query.Where(t => t.ArticleTitle.Contains(Search.ArticleTitle));
            }
            if (Search.Id   >  0)
            {
                Query = Query.Where(t => t.Id==Search.Id);
            }
            //if (!string.IsNullOrEmpty(Search.Phone))
            //{
            //    Query = Query.Where(t => t.Phone.Equals(Search.Phone));
            //}
            //var Total = await Query.CountAsync(cancellationToken);
            List<Article> QueryList = Query.ToList();
            var ResultList = _mapper.Map<List<Article>, List<ArticleDto>>(QueryList);
            var MaterialKeywords = _ArticleKeywordsRepository.Get(t=>t.Type== "Article");
            var keywords = _KeywordsRepository.GetAll();
            var users= _UserRepository.GetAll();
            var interactions = _InteractionRepository.GetAll();
            ResultList.ForEach(x =>
            {
                x.Classification = from w in keywords.Where(t => t.TypeId == "ClassIfication") 
                                   join k in keywords on w.Id equals k.ParentKey
                                   join A in MaterialKeywords.Where(t => t.MateriaArticlelId == x.Id) on k.Id equals A.KeywordsId
                                   select new Keywords()
                             {
                                 Id = k.Id,
                                 TypeId = k.TypeId,
                                 TypeName = k.TypeName
                             };
                x.Label = from w in keywords.Where(t => t.TypeId == "Label")
                          join k in keywords on w.Id equals k.ParentKey
                          join A in MaterialKeywords.Where(t => t.MateriaArticlelId == x.Id) on k.Id equals A.KeywordsId
                          select new Keywords()
                          {
                              Id = k.Id,
                              TypeId = k.TypeId,
                              TypeName = k.TypeName
                          };
                x.Special = from w in keywords.Where(t => t.TypeId == "Special")
                            join k in keywords on w.Id equals k.ParentKey
                            join A in MaterialKeywords.Where(t => t.MateriaArticlelId == x.Id) on k.Id equals A.KeywordsId
                            select new Keywords()
                            {
                                Id = k.Id,
                                TypeId = k.TypeId,
                                TypeName = k.TypeName
                            };
                x.UserInfo = users.Where(t => x.UserId==t.Id).FirstOrDefault();
                x.CollectionNum = interactions.Where(t=>t.TypeName== "Collection" && t.ArticleId==x.Id &&t.Status==true).Count();
                x.ThumbsNum = interactions.Where(t => t.TypeName == "Thumbs" && t.ArticleId == x.Id && t.Status == true).Count();
                x.BrowseNumCount = interactions.Where(t => t.TypeName == "Browse" && t.ArticleId == x.Id && t.Status == true).Count();
                if (Search.UserId>0)
                {
                    x.IsCollection = interactions.Where(t => t.TypeName == "Collection" && t.ArticleId == x.Id && t.UserId == Search.UserId).FirstOrDefault().Status;
                    x.IsFollow = interactions.Where(t => t.TypeName == "Follow" && t.ArticleId == x.Id && t.UserId == Search.UserId).FirstOrDefault().Status;
                    x.IsThumbs = interactions.Where(t => t.TypeName == "Thumbs" && t.ArticleId == x.Id && t.UserId == Search.UserId).FirstOrDefault().Status;

                }


            });
            var MaterialList = ResultList;
            if (Search.ClassIfication>0)
            {
                MaterialList = ResultList.Where(x => x.Classification.Any(t => t.Id==Search.ClassIfication)).ToList();
            }
            if (Search.Label > 0)
            {
                MaterialList = ResultList.Where(x => x.Label.Any(t => t.Id==Search.Label)).ToList();
            }
            if (Search.Special > 0)
            {
                MaterialList = ResultList.Where(x => x.Special.Any(t => t.Id==Search.Special)).ToList();
            }
            return MaterialList;
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagableData<ArticleDto>> GetArticleList(ArticleSearch Search, CancellationToken cancellationToken)
        {
            //var userId = GetUserInfoByType("Account");
            var Query = GetSearch(Search);
            var Total =  Query.Count();
            if (Search.Page > 0 && Search.Limit > 0)
            {
                if (Search.status == "hot")
                {
                    Query = Query.OrderByDescending(x => x.BrowseNumCount).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
                }
                else if (Search.status == "new")
                {
                    Query = Query.OrderByDescending(x => x.Created).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
                }
                Query = Query.OrderByDescending(x => x.ArticleCommentNum).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
            }
           // List<Article> QueryList = Query.ToList();
           // var ResultList = _mapper.Map<List<Article>, List<ArticleDto>>(QueryList);
            //ResultList.ForEach(x =>
            //{
               
            //});
            if (Query.Count() <= 0)
            {
                Total = 0;
            }
            return new PagableData<ArticleDto>
            {
                Data = Query,
                Total = Total
            };
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetArticleInfo(int Id,int UserId, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            ArticleSearch Search = new ArticleSearch()
            {
                Id = Id,
            };
            var Query = GetSearch(Search).FirstOrDefault();
           // var DataModel = await _ArticleRepository.Get(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (Query == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "文章不存在";
                return result;
            }
            //ArticleDto DataInfo = _mapper.Map<Article, ArticleDto>(DataModel);
            result.Data = Query;
            return result;
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> CreateArticle(ArticleItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<Article>(Dto);
            using (var trans = this._context.BeginTrainsaction())
            {
                _ArticleRepository.Insert(DataModel);
                await this._context.SaveChangesAsync(cancellationToken);
                trans.Commit();
                result.Message = "创建成功！";
                result.Data = DataModel;
            }
            return result;
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="Dto">文章信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> UpdateArticle(ArticleItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            try
            {

                var DataModel = _mapper.Map<Article>(Dto);
                using (var trans = this._context.BeginTrainsaction())
                {
                    _ArticleRepository.Update(DataModel);
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
        /// 删除文章
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> DeleteArticle(int Id, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            _ArticleRepository.Delete(m => m.Id == Id);
            await _context.SaveChangesAsync(cancellationToken);
            result.Message = "删除成功！";
            return result;
        }
        #endregion
        #region Extend
        /// <summary>
        /// 获取文章分类的筛选列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetArticleType(CancellationToken cancellationToken)
        {
            var type = "ArticleType";
            var result = new ResultModel();
            List<string> TypeList = type.Split(';').ToList();
            var DataList = _KeywordsRepository.Get(x => type.Contains(x.Name)).ToList();
            var ResultList = _mapper.Map<List<Keywords>, List<KeywordsDto>>(DataList);
            ResultList.ForEach(x =>
            {
                x.Keywords = _KeywordsRepository.GetAll().Where(t => t.Name.Equals("ArticleName") && x.Id == t.ParentKey).ToList();
            });
            result.Data = ResultList;
            return result;
        }
        /// <summary>
        /// 获取文章评论详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagableData<CommentDto>> GetArticleComment(int Id, CancellationToken cancellationToken)
        {
            var result = new PagableData<CommentDto>();
            //var Query = GetSearch(Search).FirstOrDefault();
            var DataModel = _CommentRepository.Get(x => x.ArticleId == Id);

            var Total = await DataModel.CountAsync(cancellationToken);
            var DataModel2 = DataModel.Where(x => x.ReplyUserId == 0).ToList();
            var Comments = _mapper.Map<List<Comment>, List<CommentDto>>(DataModel2);
            Comments.ForEach( x =>
            {
                x.FromUserInfo = _UserRepository.Get(t => t.Id == x.FromUserId).FirstOrDefault();
                 var CommentsChildren = _CommentRepository.Get(t => t.ConParentkey == x.Id).ToList();
                var Children = _mapper.Map<List<Comment>, List<CommentDto>>(CommentsChildren);
                 Children.ForEach(y =>
                {
                    y.FromUserInfo =  _UserRepository.Get(t => t.Id == y.FromUserId).FirstOrDefault();
                    y.ReplyUserInfo = _UserRepository.Get(t => t.Id == y.ReplyUserId).FirstOrDefault();
                });
                x.Children = Children;
                //join o in _CommentRepository.Get(x => x.Id == Id && x.ReplyUserId == 0) on c.Id equals o.ConParentkey

            });
            if (Comments == null)
            {
                result.Code = ResultCode.NotFound;
                return result;
            }
            //ArticleDto DataInfo = _mapper.Map<Article, ArticleDto>(DataModel);
            result.Data = Comments;
            result.Total = Total;
            return result;
        }
        /// <summary>
        /// 增加文章互动数据
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> CreateInteraction(InteractionItme Item, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<Interaction>(Item);
            using (var trans = this._context.BeginTrainsaction())
            {
                _InteractionRepository.Insert(DataModel);
                await this._context.SaveChangesAsync(cancellationToken);
                trans.Commit();
                result.Message = "增加互动信息成功！";
                result.Data = DataModel;
            }
            return result;
        }
        /// <summary>
        /// 更新互动数据
        /// </summary>
        /// <param name="Dto">文章信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> UpdateInteraction(InteractionItme Item, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var resultInteraction = await _InteractionRepository.Get(t => t.TypeName == Item.TypeName && t.UserId == Item.UserId).FirstOrDefaultAsync(cancellationToken);
            if (Item.AttentionUserId > 0)
            {
                resultInteraction = await _InteractionRepository.Get(t => t.TypeName == Item.TypeName && t.UserId == Item.UserId && t.AttentionUserId == Item.AttentionUserId).FirstOrDefaultAsync(cancellationToken);

            }
            if (resultInteraction==null)
            {
                result = await this.CreateInteraction(Item, cancellationToken);
            }
            else
            {
                try
                {
                    Item.Id= resultInteraction.Id;
                    var DataModel = _mapper.Map<Interaction>(Item);
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
            }


            

            return result;
        }
        #endregion
    }
}
