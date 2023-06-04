using AutoMapper;
using Blog.Application.Dto;
using Blog.Core.Enums;
using Blog.Core.Model;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Services.Impl
{
    public class MaterialService : ApplicationService , IMaterialService
    {
        #region init
        private readonly IRepository<Material> _MaterialRepository;
        private readonly IRepository<Keywords> _KeywordsRepository;
        private readonly IRepository<MaterialArticleKeywords> _MaterialKeywordsRepository;
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Dictionary> _DictionaryRepository;
        private readonly IRepository<Interaction> _InteractionRepository;
        public MaterialService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._MaterialRepository = this._repositoryProvider.Create<Material>(this._context);
            this._KeywordsRepository = this._repositoryProvider.Create<Keywords>(this._context);
            this._MaterialKeywordsRepository = this._repositoryProvider.Create<MaterialArticleKeywords>(this._context);
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
            this._UserRepository = this._repositoryProvider.Create<User>(this._context);
            this._InteractionRepository = this._repositoryProvider.Create<Interaction>(this._context);

        }
        #endregion
        #region Public
        public List<MaterialDto> GetSearch(MaterialSearch Search)
        {
            //var Post = GetUserInfoByType("Post");
            //var Account = GetUserInfoByType("Account");
            //var Province = GetUserInfoByType("Province");
            //var City = GetUserInfoByType("City");
            // var County = GetUserInfoByType("County");
            var interactions = _InteractionRepository.GetAll();
            var Query = this._MaterialRepository.GetAll();
            if (Search.Id>0)
            {
                Query = Query.Where(t => t.Id== Search.Id);
            }
 
            if (!string.IsNullOrEmpty(Search.MaterialName))
            {
                Query = Query.Where(t => t.MaterialName.Contains(Search.MaterialName));
            }
            //if (!string.IsNullOrEmpty(Search.Phone))
            //{
            //    Query = Query.Where(t => t.Phone.Equals(Search.Phone));
            //}
            //var Total = await Query.CountAsync(cancellationToken);
            List<Material> QueryList = Query.ToList();
            var ResultList = _mapper.Map<List<Material>, List<MaterialDto>>(QueryList);
            ResultList.ForEach(x =>
            {
                x.Keywords = from A in _MaterialKeywordsRepository.GetAll().Where(t => t.MateriaArticlelId == x.Id&& t.Type=="Materia")
                             join k in _KeywordsRepository.GetAll() on A.KeywordsId equals k.Id
                             select new Keywords()
                             {
                                 Id = k.Id,
                                 TypeId = k.TypeId,
                                 TypeName = k.TypeName
                             };
                x.LikeNum= interactions.Where(t => t.TypeName == "LikeMaterial" && t.ArticleId == x.Id && t.Status == true).Count();
                x.BrowseNum = interactions.Where(t => t.TypeName == "BrowseMaterial" && t.ArticleId == x.Id && t.Status == true).Count();
                if (Search.UserId > 0)
                {
                    x.IsLike = interactions.Where(t => t.TypeName == "Collection" && t.ArticleId == x.Id && t.UserId == Search.UserId).FirstOrDefault().Status;
                }
               
            });
            var MaterialList = ResultList;
            if (!string.IsNullOrEmpty(Search.keywords))
            {
                MaterialList = ResultList.Where(x => x.Keywords.Any(t => t.TypeName.Equals(Search.keywords))).ToList();
            }
            return MaterialList;
        }
        public async Task<PagableData<MaterialDto>> GetMaterialList(MaterialSearch Search, CancellationToken cancellationToken)
        {
            try
            {
            var userId = GetUserInfoByType("Account");
            var Query = GetSearch(Search);

                if (Search.Page > 0 && Search.Limit > 0)
                {
                    if (Search.sort=="hot")
                    {
                        Query = Query.OrderByDescending(x => x.BrowseNum).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
                    }else if(Search.sort == "new")
                    {
                        Query = Query.OrderByDescending(x => x.Created).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
                    }
                    Query = Query.OrderByDescending(x => x.LikeNum).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
                }
                var Total = Query.Count();
                if (Query.Count() <= 0)
            {
                Total = 0;
            }
                //Keywords newKeywords = new Keywords();
                //newKeywords.TypeName = "C#";
                //foreach (var item in ResultList.ToList())
                //{
                //    var a = item.Keywords.Contains(newKeywords);
                //}
             return new PagableData<MaterialDto>
            {
                Data = Query,
                Total = Total
            };
            }
            catch (Exception ee)
            {

                throw ee;
            }
        }
        public async Task<PagableData<MaterialDto>> GetMaterialKeywordsList(MaterialSearch Search, CancellationToken cancellationToken)
        {
            var userId = GetUserInfoByType("Account");
            var Query = GetSearch(Search);
            var Total = Query.Count();
            if (Search.Page > 0 && Search.Limit > 0)
            {
                Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit).ToList();
            }
            List<MaterialDto> ResultList = Query;
            //var ResultList = _mapper.Map<List<Material>, List<MaterialDto>>(QueryList);
            ResultList.ForEach(x =>
            {

            });
            if (Query.Count() <= 0)
            {
                Total = 0;
            }
            return new PagableData<MaterialDto>
            {
                Data = ResultList,
                Total = Total
            };
        }
        /// <summary>
        /// 提取一个info
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetMaterialInfo(MaterialSearch Search, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            var Query = GetSearch(Search);
            //var DataModel = await _MaterialRepository.Get(x => x.Id == Search.Id).FirstOrDefaultAsync(cancellationToken);
            var DataModel = Query.FirstOrDefault();
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "不存在";
                return result;
            }
            DataModel.User = _UserRepository.Get(t => t.Id == DataModel.Userid);
           // MaterialDto DataInfo = _mapper.Map<Material, MaterialDto>(DataModel);
            result.Data = DataModel;
            return result;
        }
        public async Task<ResultModel> CreateMaterial(MaterialItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<Material>(Dto);
            using (var trans = this._context.BeginTrainsaction())
            {
                _MaterialRepository.Insert(DataModel);
                await this._context.SaveChangesAsync(cancellationToken);
                trans.Commit();
                result.Message = "创建成功！";
                result.Data = DataModel;
            }
            return result;
        }

        public async Task<ResultModel> UpdateMaterial(MaterialItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            try
            {

                var DataModel = _mapper.Map<Material>(Dto);
                using (var trans = this._context.BeginTrainsaction())
                {
                    _MaterialRepository.Update(DataModel);
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

        public async Task<ResultModel> DeleteMaterial(int Id, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            _MaterialRepository.Delete(m => m.Id == Id);
            await _context.SaveChangesAsync(cancellationToken);
            result.Message = "删除成功！";
            return result;
        }

        #endregion
        #region Extend
        public async Task<ResultModel> GetMaterialType(CancellationToken cancellationToken)
        {
            var type = "MaterialType";
            var result = new ResultModel();
            List<string> TypeList = type.Split(';').ToList();
            var DataList = _KeywordsRepository.Get(x => type.Contains(x.Name)).ToList();
            var ResultList = _mapper.Map<List<Keywords>, List<KeywordsDto>>(DataList);
            ResultList.ForEach(x =>
            {
                x.Keywords = _KeywordsRepository.GetAll().Where(t => t.Name.Equals("MaterialName") && x.Id == t.ParentKey).ToList();
            });
            //var DataList = from    d in _DictionaryRepository.Get(x => TypeList.Contains(x.Name))
            //              join k in _KeywordsRepository.GetAll() on d.Id.ToString() equals k.TypeId
            //               select new DictionaryDto()
            //               {
            //                   Id= d.Id,
            //                   Value=d.Value,
            //                   Name=d.Name,
            //                   Key=d.Key,
            //                   Keywords= _KeywordsRepository.GetAll().Where(t=>t.TypeId.Equals(d.Id)).ToList(),
            //               };
            //var  DataList11 = DataList.ToList();
            // var ResultList = _mapper.Map<List<Dictionary>, List<DictionaryDto>>(DataList);
            result.Data = ResultList;
            return result;
        }
        #endregion


    }
}
