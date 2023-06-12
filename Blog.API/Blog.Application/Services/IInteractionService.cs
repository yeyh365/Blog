using Blog.Application.Dto;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public interface IInteractionService
    {
        #region Public
        /// <summary>
        /// 获取联系我们列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagableData<InteractionDto>> GetInteractionList(InteractionSearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 获取联系我们详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetInteractionInfo(string TypeName, int ArticleId, int UserId, CancellationToken cancellationToken);

        /// <summary>
        /// 创建联系我们
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateInteraction(InteractionItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 更新联系我们
        /// </summary>
        /// <param name="Dto">联系我们信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateInteraction(InteractionItem Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除联系我们
        /// </summary>
        /// <param name="Id">联系我们id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteInteraction(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend

        #endregion
    }
}
