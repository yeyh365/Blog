

namespace Blog.Domain.DBFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IDeleteFilter
    {
        bool Deleted { get; set; }
    }
}
