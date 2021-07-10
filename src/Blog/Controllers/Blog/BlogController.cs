using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Core.Exceptions;
using Blog.Service.Blog.Article;
using Blog.Service.Blog.Article.Input;
using Blog.Service.Blog.Article.Output;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog
{
    [ApiController]
    [Route("api/blog")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v1)]
    public class BlogController
    {
        private readonly IArticleSvc _articleSvc;

        public BlogController(IArticleSvc articleSvc)
        {
            _articleSvc = articleSvc;
        }
        /// <summary>
        /// 获取文章分页列表
        /// </summary>
        /// <param name="pagingDto">分页条件</param>
        [HttpGet("pages")]
        public async Task<ServiceResult<PagedDto<ArticleDto>>> GetPagesAsync([FromQuery] ArticlePagingDto pagingDto)
        {
            if (pagingDto.CreateTimeStart != null && pagingDto.CreateTimeEnd == null)
                return ServiceResult<PagedDto<ArticleDto>>.Failed("创建时间起止时间有误");
            return await Task.FromResult(ServiceResult<PagedDto<ArticleDto>>.Successed(await _articleSvc.GetPagesAsync(pagingDto)));
        }
    }
}
