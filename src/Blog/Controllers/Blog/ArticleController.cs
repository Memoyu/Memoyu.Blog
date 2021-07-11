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
    /// <summary>
    /// 文章
    /// </summary>
    [ApiController]
    [Route("api/article")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v1)]
    public class ArticleController
    {
        private readonly IArticleSvc _articleSvc;

        public ArticleController(IArticleSvc articleSvc)
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
            var validation = pagingDto.Validation();
            if (validation.Fail) return ServiceResult<PagedDto<ArticleDto>>.Failed(validation.Msg);
            return await Task.FromResult(await _articleSvc.GetPagesAsync(pagingDto));
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id">分页条件</param>
        [HttpGet("{id}")]
        public async Task<ServiceResult<ArticleContentDto>> GetAsync(long id)
        {
            if (id <= 0)
                return ServiceResult<ArticleContentDto>.Failed("id不能小于等于0");
            return await Task.FromResult(await _articleSvc.GetAsync(id));
        }
    }
}
