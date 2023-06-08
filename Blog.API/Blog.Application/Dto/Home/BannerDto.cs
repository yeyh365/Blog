using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto.Home
{
    public class BannerDto
    {

        public int? Id { get; set; }
        public string? BannerName { get; set; }
        public string? BannerUrl { get; set; }
        public int? IsLink { get; set; }
        public string? LinkUrl { get; set; }
        public int? IsMaterial { get; set; }
        public int? QuoteId { get; set; }
        public int? Status { get; set; }
        public int? Sort { get; set;}
        public string? Describe { get; set;}

        public DateTime? Created { get; set;}




    }
}
