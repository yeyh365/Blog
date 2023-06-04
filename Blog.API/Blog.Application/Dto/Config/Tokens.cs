using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class Tokens
    {

        public string access_token { get; set; }
        public DateTime? expires_in { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
