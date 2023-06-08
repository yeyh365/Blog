using Blog.Application.Dto.Home;
using Blog.Application.Dto;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public interface IHomeService
    {
        Task<PagableData<BannerDto>> GetRecommendInfo(CancellationToken cancellationToken);

    }
}
