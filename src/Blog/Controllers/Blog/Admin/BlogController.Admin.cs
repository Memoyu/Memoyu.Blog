using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Core.Domains.Common.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog.Admin
{
    [ApiController]
    [Route("api/admin/blog")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v2)]
    [Authorize]
    public class BlogController
    {
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
