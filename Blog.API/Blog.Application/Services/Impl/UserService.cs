
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
    /// UserService
    /// </summary>
    public class UserService : ApplicationService, IUserService
    {
        #region init
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Dictionary> _DictionaryRepository;
        /// <summary>
        /// UserService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public UserService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._UserRepository = this._repositoryProvider.Create<User>(this._context);
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
        }
        #endregion
        #region Public
        public IQueryable<User> GetSearch(UserSearch Search)
        {
            var Post = GetUserInfoByType("Post");
            var Account = GetUserInfoByType("Account");
            var Province = GetUserInfoByType("Province");
            var City = GetUserInfoByType("City");
            var County = GetUserInfoByType("County");

            var Query = this._UserRepository.GetAll();
            if (!string.IsNullOrEmpty(Search.Name))
            {
                Query = Query.Where(t => t.Name.Contains(Search.Name));
            }
            if (!string.IsNullOrEmpty(Search.Account))
            {
                Query = Query.Where(t => t.Account.Contains(Search.Account));
            }
            if (!string.IsNullOrEmpty(Search.Post))
            {
                Query = Query.Where(t => t.Post.Contains(Search.Post));
            }
            
            if (!string.IsNullOrEmpty(Post))
            {
                List<string> ListName = new List<string>
                {
                    "Province",
                    "City",
                    "County"
                };
                var List = _DictionaryRepository.Get(s => ListName.Contains(s.Name)).ToList();
                if (Post== "ProvinceAdmin")
                {
                    var ParentID = List.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault().Key;
                    var CityList = List.Where(s => s.Name == "City" && s.ParentKey == ParentID).Select(s => s.Value).ToList();
                    var CityKeyList = List.Where(s => s.Name == "City" && s.ParentKey == ParentID).Select(s => s.Key).ToList();
                    var CountyList = List.Where(s => s.Name == "County" && CityKeyList.Contains(s.ParentKey)).Select(s => s.Value).ToList();
                    Query = Query.Where(t => t.Account== Account || (t.Post== "CityAdmin" && CityList.Contains(t.City)) || (t.Post == "CountyAdmin" && CountyList.Contains(t.County)) || (t.Post == "CountyUser" && CountyList.Contains(t.County)));
                }
                else if (Post == "CityAdmin")
                {
                    var ParentID = List.Where(s => s.Name == "City" && s.Value == City).FirstOrDefault().Key;
                    var CountyList = List.Where(s => s.Name == "County" && s.ParentKey== ParentID).Select(s => s.Value).ToList();
                    Query = Query.Where(t => t.Account == Account || (t.Post == "CountyAdmin" && CountyList.Contains(t.County)) || (t.Post == "CountyUser" && CountyList.Contains(t.County)));
                }
                else if (Post == "CountyAdmin")
                {
                    Query = Query.Where(t => t.Account == Account || (t.Post == "CountyUser" && t.County== County));
                }
            }

            return Query;
        }
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagableData<UserDto>> GetUserList(UserSearch Search, CancellationToken cancellationToken)
        {
            var userId = GetUserInfoByType("Account");
            var Query = GetSearch(Search);
            var Total = await Query.CountAsync(cancellationToken);
            if (Search.Page > 0 && Search.Limit > 0)
            {
                Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            }
            List<User> QueryList = Query.ToList();
            var ResultList = _mapper.Map<List<User>, List<UserDto>>(QueryList);
            ResultList.ForEach(x =>
            {
                x.PassWord = "";
              if(!string.IsNullOrEmpty(x.Post))
                {
                    var info = _DictionaryRepository.Get(s => s.Name == "Role" && s.Key == x.Post).FirstOrDefault();
                    if (info != null) {
                    x.PostName= info.Value;
                    } else{
                        x.PostName = x.Post;
                    };
                }
            });
            if (QueryList.Count() <= 0)
            {
                Total = 0;
            }
            return new PagableData<UserDto>
            {
                Data = ResultList,
                Total = Total
            };
        }

        /// <summary>
        /// 获取管理员详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetUserInfo(int Id, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            var DataModel = await _UserRepository.Get(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "管理员不存在";
                return result;
            }
            UserDto DataInfo = _mapper.Map<User, UserDto>(DataModel);
            result.Data = DataInfo;
            return result;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> CreateUser(UserItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<User>(Dto);
            var CheckModel = this._UserRepository.Get(x => x.Name == DataModel.Name).FirstOrDefault();
            if (CheckModel != null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "管理员已存在";
                return result;
            }
            using (var trans = this._context.BeginTrainsaction())
            {
                DataModel.PassWord = MD5Helper.Md5Method(DataModel.PassWord);
                _UserRepository.Insert(DataModel);
                await this._context.SaveChangesAsync(cancellationToken);
                trans.Commit();
                result.Message = "创建成功！";
                result.Data = DataModel;
            }
            return result;
        }

        /// <summary>
        /// 更新管理员
        /// </summary>
        /// <param name="Dto">管理员信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> UpdateUser(UserItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            try
            {

                var DataModel = _mapper.Map<User>(Dto);
                var CheckModel = this._UserRepository.Get(x => x.Name == DataModel.Name && x.Id != DataModel.Id).FirstOrDefault();
                if (CheckModel != null)
                {
                    result.Code = ResultCode.NotFound;
                    result.Message = "管理员已存在";
                    return result;
                }
                using (var trans = this._context.BeginTrainsaction())
                {
                    if(!string.IsNullOrEmpty(DataModel.PassWord))
                    {
                        DataModel.PassWord = MD5Helper.Md5Method(DataModel.PassWord);
                    }
                    else
                    {
                        var info = this._UserRepository.Get(x => x.Id == DataModel.Id).FirstOrDefault();
                        DataModel.PassWord = info.PassWord;
                    }
                    
                    _UserRepository.Update(DataModel);
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
        /// 删除管理员
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> DeleteUser(int Id, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            _UserRepository.Delete(m => m.Id == Id);
            await _context.SaveChangesAsync(cancellationToken);
            result.Message = "删除成功！";
            return result;
        }
        #endregion
        #region Extend
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public UserDto Login(UserLogin Dto)
        {
            var Admin =  _UserRepository.Get(u => u.Account == Dto.Account && u.PassWord == Dto.PassWord).FirstOrDefault();
            UserDto result = new UserDto();
            if (Admin != null)
            {
                result = _mapper.Map<User, UserDto>(Admin);
            }
            else
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// 获取管理员详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> FindLoginUser(CancellationToken cancellationToken)
        {
            var result = new ResultModel();

            string Account = GetUserInfoByType("Account");

            var DataModel = await _UserRepository.Get(x => x.Account == Account).FirstOrDefaultAsync(cancellationToken);
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "管理员不存在";
                return result;
            }
            UserDto DataInfo = _mapper.Map<User, UserDto>(DataModel);
            try
            {
                List<string> TypeList = "Province;City;County".Split(';').ToList();
                //权限控制
                var DataList = _DictionaryRepository.Get(x => TypeList.Contains(x.Name)).ToList();
                List<int> Ids = GetDictionaryIds(DataList);
                DataList = DataList.Where(s => Ids.Contains(s.Id)).ToList();
                var ResultList = _mapper.Map<List<Dictionary>, List<DictionaryDto>>(DataList);
                DataInfo.DicList = ResultList;
            }
            catch (Exception ex) { }
            result.Data = DataInfo;
            

            return result;
        }

        #endregion
    }
}
