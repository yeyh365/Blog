using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Enums;

namespace Blog.Core.Model
{
    public class ResultModel<T> : ResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ResultModel() : base() { }

        public ResultModel(ResultCode code) : base(code) { }

        public new T Data { get; set; }
    }


    public class ResultModel
    {
        public ResultModel()
        {
            Code = ResultCode.Success;
        }
        public ResultModel(ResultCode code)
        {
            Code = code;
        }

        public ResultCode Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
