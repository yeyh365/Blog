using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class ArticleSearch:SearchCommon
    {
        public int? Id { get; set; }
         public int? UserId { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleContent { get; set; }

        public int? ClassIfication { get; set;}
        public int? Label { get; set; }
        public int? Special { get; set; }
        public string? status { get; set; }

        
    }
}
