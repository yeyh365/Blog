using Blog.Application.Dto;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public interface IMaterialService
    {
        #region Public
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<MaterialDto>> GetMaterialList(MaterialSearch Search, CancellationToken cancellationToken);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<MaterialDto>> GetMaterialKeywordsList(MaterialSearch Search, CancellationToken cancellationToken);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetMaterialInfo(MaterialSearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateMaterial(MaterialItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Dto">联系我们信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateMaterial(MaterialItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">联系我们id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteMaterial(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend
        Task<ResultModel> GetMaterialType(CancellationToken cancellationToken);
        #endregion
    }
}
