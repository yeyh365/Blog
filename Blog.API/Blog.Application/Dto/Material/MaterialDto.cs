using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class MaterialDto: Material
    {
        public IQueryable<Keywords> Keywords { get; set; }
        public IQueryable<User> User { get; set; }
        public bool? IsLike { get; set; }
    }
}
