namespace Blog.Application.Services
{
    using Blog.Domain.Entities;
    using Blog.Domain.Repositories;
    using Blog.Application.Dto;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Blog.Core.Model;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Caching.Memory;
    using System.ComponentModel;

    /// <summary>
    /// ApplicationService
    /// </summary>
    public abstract class ApplicationService
    {
        /// <summary>
        /// _context
        /// </summary>
        protected readonly IDBContext _context;
        /// <summary>
        /// _repositoryProvider
        /// </summary>
        protected readonly IRepositoryProvider _repositoryProvider;
        /// <summary>
        /// _mapper
        /// </summary>
        protected readonly IMapper _mapper;
        /// <summary>
        /// _httpContext
        /// </summary>
        protected readonly IHttpContextAccessor _httpContext;
        /// <summary>
        /// _httpContext
        /// </summary>
        protected readonly IConfiguration _Configuration;
        /// <summary>
        /// _httpContext
        /// </summary>
        protected readonly IMemoryCache _Cache;

        public Dictionary<string, string> dicColumn = new Dictionary<string, string>();

        /// <summary>
        /// ApplicationService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public ApplicationService(
            IDBContext context,
            IRepositoryProvider repositoryProvider,
            IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        /// <summary>
        /// ApplicationService(重载_Configuration)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        /// <param name="Configuration"></param>
        public ApplicationService(
            IDBContext context,
            IRepositoryProvider repositoryProvider,
            IMapper mapper, IHttpContextAccessor httpContext, IConfiguration Configuration)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _httpContext = httpContext;
            _Configuration = Configuration;
        }
        public ApplicationService(
    IDBContext context,
    IRepositoryProvider repositoryProvider,
    IMapper mapper, IHttpContextAccessor httpContext, IConfiguration Configuration, IMemoryCache Cache)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _httpContext = httpContext;
            _Configuration = Configuration;
            _Cache= Cache;
        }

        /// <summary>
        /// ApplicationService(重载_Configuration)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        /// <param name="Configuration"></param>
        public ApplicationService(IDBContext context,
            IRepositoryProvider repositoryProvider, IConfiguration Configuration, IMemoryCache Cache, IMapper mapper)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _Configuration = Configuration;
            _Cache = Cache;
            _mapper = mapper;
        }
        public ApplicationService(IHttpClientFactory clientFactory , IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Type">Type</param>
        /// <returns></returns>
        public string GetUserInfoByType(string Type)
        {
            try
            {
                var info = _httpContext.HttpContext.User.Claims.Where(x => x.Type == Type).FirstOrDefault();
                if (info != null)
                {
                    return _httpContext.HttpContext.User.Claims.Where(x => x.Type == Type).FirstOrDefault().Value;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ee)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取Ids
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        public List<int> GetDictionaryIds(List<Dictionary> DataList)
        {
            //权限控制
            var Post = GetUserInfoByType("Post");
            var Account = GetUserInfoByType("Account");
            var Province = GetUserInfoByType("Province");
            var City = GetUserInfoByType("City");
            var County = GetUserInfoByType("County");
            List<int> Ids = new List<int>();
            //if (!string.IsNullOrEmpty(Post) && Post != "SuperAdmin")
            //{
                
            //    if (Post == "ProvinceAdmin")
            //    {
            //        var ProvinceInfo = DataList.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault();
            //        if (ProvinceInfo != null)
            //        {
            //            Ids.Add(ProvinceInfo.Id);
            //            var CityList = DataList.Where(s => s.Name == "City" && s.ParentKey == ProvinceInfo.Key).ToList();
            //            if (CityList != null)
            //            {
            //                Ids.AddRange(CityList.Select(s => s.Id).ToList());
            //                var CountyList = DataList.Where(s => s.Name == "County" && CityList.Select(s => s.Key).ToList().Contains(s.ParentKey)).ToList();
            //                if (CountyList != null)
            //                {
            //                    Ids.AddRange(CountyList.Select(s => s.Id).ToList());
            //                }
            //            }
            //        }
            //    }
            //    else if (Post == "CityAdmin")
            //    {
            //        var ProvinceInfo = DataList.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault();
            //        if (ProvinceInfo != null)
            //        {
            //            Ids.Add(ProvinceInfo.Id);
            //            var CityInfo = DataList.Where(s => s.Name == "City" && s.Value == City).FirstOrDefault();
            //            if (CityInfo != null)
            //            {
            //                Ids.Add(CityInfo.Id);
            //                var CountyList = DataList.Where(s => s.Name == "County" && s.ParentKey == CityInfo.Key).ToList();
            //                if (CountyList != null)
            //                {
            //                    Ids.AddRange(CountyList.Select(s => s.Id).ToList());
            //                }
            //            }
            //        }
            //    }
            //    else if (Post == "CountyAdmin" || Post == "CountyUser")
            //    {
            //        var ProvinceInfo = DataList.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault();
            //        if (ProvinceInfo != null)
            //        {
            //            Ids.Add(ProvinceInfo.Id);
            //            var CityInfo = DataList.Where(s => s.Name == "City" && s.Value == City).FirstOrDefault();
            //            if (CityInfo != null)
            //            {
            //                Ids.Add(CityInfo.Id);
            //                var CountyInfo = DataList.Where(s => s.Name == "County" && s.Value == County).FirstOrDefault();
            //                if (CountyInfo != null)
            //                {
            //                    Ids.Add(CountyInfo.Id);
            //                }
            //            }
            //        }
            //    }
            //    DataList = DataList.Where(s => Ids.Contains(s.Id)).ToList();
            //}
            //else
            //{
            //    Ids = DataList.Select(s => s.Id).ToList();
            //}

            return Ids;
        }

        /// <summary>
        /// 获取Ids
        /// </summary>
        /// <param name="Search"></param>
        /// <returns></returns>
        public List<int> GetDictionaryIdsNew(List<Dictionary> DataList)
        {
            //权限控制
            var Post = GetUserInfoByType("Post");
            var Account = GetUserInfoByType("Account");
            var Province = GetUserInfoByType("Province");
            var City = GetUserInfoByType("City");
            var County = GetUserInfoByType("County");
            List<int> Ids = new List<int>();
            if (!string.IsNullOrEmpty(Post) && Post != "SuperAdmin")
            {

                if (Post == "ProvinceAdmin")
                {
                    var ProvinceInfo = DataList.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault();
                    if (ProvinceInfo != null)
                    {
                        Ids.Add(ProvinceInfo.Id);
                    }
                }
                else if (Post == "CityAdmin")
                {
                    var ProvinceInfo = DataList.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault();
                    if (ProvinceInfo != null)
                    {
                        Ids.Add(ProvinceInfo.Id);
                        var CityInfo = DataList.Where(s => s.Name == "City" && s.Value == City).FirstOrDefault();
                        if (CityInfo != null)
                        {
                            Ids.Add(CityInfo.Id);
                        }
                    }
                }
                else if (Post == "CountyAdmin" || Post == "CountyUser")
                {
                    var ProvinceInfo = DataList.Where(s => s.Name == "Province" && s.Value == Province).FirstOrDefault();
                    if (ProvinceInfo != null)
                    {
                        Ids.Add(ProvinceInfo.Id);
                        var CityInfo = DataList.Where(s => s.Name == "City" && s.Value == City).FirstOrDefault();
                        if (CityInfo != null)
                        {
                            Ids.Add(CityInfo.Id);
                            var CountyInfo = DataList.Where(s => s.Name == "County" && s.Value == County).FirstOrDefault();
                            if (CountyInfo != null)
                            {
                                Ids.Add(CountyInfo.Id);
                            }
                        }
                    }
                }
                DataList = DataList.Where(s => Ids.Contains(s.Id)).ToList();
            }
            else
            {
                Ids = new List<int>();
            }

            return Ids;
        }
    }
}