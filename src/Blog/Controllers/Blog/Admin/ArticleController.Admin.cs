using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Controllers.Core;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Core.Domains.Common.Enums.Base;
using Blog.Service.Blog.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog.Admin
{
    [Route("api/admin/article")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v2)]
    [Authorize]
    public class ArticleController : ApiControllerBase
    {
        private readonly IArticleSvc _articleSvc;

        public ArticleController(IArticleSvc articleSvc)
        {
            _articleSvc = articleSvc;
        }

        /// <summary>
        /// 根据URL获取文章详情
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("post")]
        public async Task<ServiceResult<string>> GetPostDetailAsync([FromQuery] string url)
        {
            return await Task.FromResult(ServiceResult<string>.Successed("Message"));
        }
    }
}
