

namespace Blog.Domain.DBFilter
{
    using System;
    public interface IEntityCreate
    {
        /// <summary>
        /// 创建者的ID
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? Created { get; set; }
    }
}
