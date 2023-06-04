using Blog.Core.Enums;
namespace Blog.Core.Model
{
    using System.Collections.Generic;

    public class PagableData<T>
    {
        public PagableData()
        {
            Code = ResultCode.Success;
        }

        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }

        public ResultCode Code { get; set; }



    }
    public class PagableData
    {
        public PagableData()
        {
            Code = ResultCode.Success;
        }

        public dynamic Data { get; set; }

        public int Total { get; set; }

        public ResultCode Code { get; set; }



    }
}