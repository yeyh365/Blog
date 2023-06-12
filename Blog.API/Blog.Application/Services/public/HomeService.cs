using AutoMapper;
using Blog.Application.Dto;
using Blog.Application.Dto.Home;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Application.Services
{
    public class HomeService : ApplicationService, IHomeService
    {
        #region init
        private readonly IRepository<Material> _MaterialRepository;
        private readonly IRepository<Keywords> _KeywordsRepository;
        private readonly IRepository<MaterialArticleKeywords> _MaterialKeywordsRepository;
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Dictionary> _DictionaryRepository;
        private readonly IRepository<Interaction> _InteractionRepository;
        private readonly IRepository<Article> _ArticleRepository;
        public HomeService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._MaterialRepository = this._repositoryProvider.Create<Material>(this._context);
            this._KeywordsRepository = this._repositoryProvider.Create<Keywords>(this._context);
            this._MaterialKeywordsRepository = this._repositoryProvider.Create<MaterialArticleKeywords>(this._context);
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
            this._UserRepository = this._repositoryProvider.Create<User>(this._context);
            this._InteractionRepository = this._repositoryProvider.Create<Interaction>(this._context);
            this._ArticleRepository = this._repositoryProvider.Create<Article>(this._context);

        }
        #endregion
        #region pulic
        //public List<BannerDto> GetSearch(ContactUsSearch Search)
        //{
        //    //var Post = GetUserInfoByType("Post");
        //    //var Account = GetUserInfoByType("Account");
        //    //var Province = GetUserInfoByType("Province");
        //    //var City = GetUserInfoByType("City");
        //    //var County = GetUserInfoByType("County");
        //    list<BannerDto>    bannerDto = new BannerDto();
        //    var Query = this._MaterialRepository.GetAll();
        //    //if (!string.IsNullOrEmpty(Search.Title))
        //    //{
        //    //    Query = Query.Where(t => t.Title.Contains(Search.Title));
        //    //}
        //    //if (!string.IsNullOrEmpty(Search.Phone))
        //    //{
        //    //    Query = Query.Where(t => t.Phone.Equals(Search.Phone));
        //    //}
        //    return bannerDto;
        //}
        /// <summary>
        /// 获取推荐列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagableData<BannerDto>> GetRecommendInfo(CancellationToken cancellationToken)
        {
            List<BannerDto> listBanner= new List<BannerDto> { };
            var material = (from m in this._MaterialRepository.GetAll().ToList()
                           join d in _InteractionRepository.Get(t => t.TypeName == "LikeMaterial").ToList() on m.Id equals d.ArticleId
                          into bGroup
                           select new {
                               TableAId=m.Id,
                               BCount= bGroup.Count()
                           }).ToList();
            var info = material.OrderByDescending(t => t.BCount).FirstOrDefault();
            //var Id = material.OrderByDescending(t => t.BCount).FirstOrDefault()?.TableAId ?? 0;
            if (info !=null)
            {
                var materialInfo = this._MaterialRepository.Get(t => t.Id == info.TableAId).FirstOrDefault();
                BannerDto bannerMaterial = new BannerDto();
                bannerMaterial.Id = materialInfo.Id;
             bannerMaterial.BannerName =materialInfo.MaterialName;
             bannerMaterial.BannerUrl  = "UploadFiles\\Photos\\230608151911_a5c2eb11-7eff-447d-8d6c-076d6b854361.jpg";
             bannerMaterial.IsLink     =0;
             bannerMaterial.IsMaterial =1;
             bannerMaterial.QuoteId    =materialInfo.Id;
             bannerMaterial.Status     =materialInfo.Status;
             bannerMaterial.Describe = materialInfo.MaterialDescribe;
              listBanner.Add(bannerMaterial);
            }
            var article = (from m in this._ArticleRepository.GetAll().ToList()
                           join d in _InteractionRepository.Get(t => t.TypeName == "Thumbs") on m.Id equals d.ArticleId
                          into aGroup
                           select new
                           {
                               TableAId = m.Id,
                               BCount = aGroup.Count()
                           }).ToList();
            var articleFirst = article.OrderByDescending(t => t.BCount).FirstOrDefault();
            if (articleFirst != null)
            {
                var articleInfo = this._ArticleRepository.Get(t => t.Id == articleFirst.TableAId).FirstOrDefault();
                BannerDto bannerArticle = new BannerDto();
                bannerArticle.Id = articleInfo.Id;
                bannerArticle.BannerName = articleInfo.ArticleTitle;
                bannerArticle.BannerUrl = articleInfo.CoverImgUrl;
                bannerArticle.IsLink = 0;
                bannerArticle.IsMaterial = 0;
                bannerArticle.QuoteId = articleInfo.Id;
                bannerArticle.Status = articleInfo.Status;
                bannerArticle.Describe = articleInfo.ArticleContent;
                listBanner.Add(bannerArticle);
            }
            BannerDto bannerlink = new BannerDto();
            bannerlink.Id =999;
            bannerlink.BannerName = "极简壁纸";
            bannerlink.BannerUrl = "UploadFiles\\Photos\\230608173618_69aa188f-0534-40ec-830c-b198928b96c9.jpg";
            bannerlink.IsLink = 1;
            bannerlink.LinkUrl = "https://bz.zzzmh.cn/index#anime";
            bannerlink.Status = 1;
            bannerlink.Describe = "免费、高清、炫酷！";
            listBanner.Add(bannerlink);
            //var article = this._ArticleRepository.GetAll();

            //var userId = GetUserInfoByType("Account");
            // var Query = GetSearch(Search);
            var Total =  listBanner.Count();
            //var Total = await Query.CountAsync(cancellationToken);
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            //List<BannerDto> QueryList = Query.ToList();
            //ResultList.ForEach(x =>
            //{

            //});
            if (listBanner.Count() <= 0)
            {
                Total = 0;
            }
            return new PagableData<BannerDto>
            {
                Data = listBanner,
                Total = Total
            };
        }
        #endregion
    }
}
