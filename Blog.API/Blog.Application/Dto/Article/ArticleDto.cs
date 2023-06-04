using Blog.Application.Services;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class ArticleDto: Article
    {
        public User UserInfo { get; set; }
        public IQueryable<Keywords> Classification { get; set; }
        public IQueryable<Keywords> Label { get; set; }
        public IQueryable<Keywords> Special { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int ArticleCommentNum { get; set; }
        public int CollectionNum { get; set; }

        public int ThumbsNum { get; set; }

        public int BrowseNumCount { get; set; }

        public bool? IsCollection { get; set; }
        public bool? IsFollow { get; set; }
        public bool? IsThumbs { get; set; }
        public Comment Comments { get; set; }








    }
}
