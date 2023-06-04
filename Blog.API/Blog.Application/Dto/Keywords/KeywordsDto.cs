using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class KeywordsDto : Keywords
    {
        public List<Keywords> Keywords { get; set; }
    }
}
