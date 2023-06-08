using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dto
{
    public class MaterialItem
    {
        public int? Id { get; set; }

        public int? BrowseNum { get; set; }

        public int? LikeNum { get; set; }

        public string? MaterialCover { get; set; } = string.Empty;

        public int? MaterialCoverid { get; set; }

        public string? MaterialDescribe { get; set; } = string.Empty;

        public string? MaterialDetails { get; set; } = string.Empty;
        public string? MaterialLink { get; set; } = string.Empty;

        public string? MaterialName { get; set; }

        public int? Status { get; set; }

        public int? Userid { get; set; }
        public string? BannerUrl { get; set; } = string.Empty;

        public string? Remark { get; set; } = string.Empty;
        public int[]? Label { get; set; }
    }
}
