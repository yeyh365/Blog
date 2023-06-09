﻿

namespace Blog.Domain.DBFilter
{
    using System;
    public interface IEntityUpdate
    {
        /// <summary>
        /// 更新者的ID，第一次创建时，创建者和更新者为同一个ID
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// 更新时间，第一次创建时，创建时间和更新时间相同
        /// </summary>
        DateTime? Updated { get; set; }
    }
}
