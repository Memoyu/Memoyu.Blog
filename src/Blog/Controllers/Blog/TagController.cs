using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Controllers.Core;
using Blog.Core.AOP.Attributes;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Service.Blog.Tag;
using Blog.Service.Blog.Tag.Input;
using Blog.Service.Blog.Tag.Output;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog
{
    /// <summary>
    /// 文章标签
    /// </summary>
    [Route("api/tag")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v1)]
    public class TagController : ApiControllerBase
    {
        private readonly ITagSvc _tagSvc;

        public TagController(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        /// <summary>
        /// 获取文章标签
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ServiceResult<TagTotalDto>> GetAsync(long id)
        {
            if (id <= 0)
                return ServiceResult<TagTotalDto>.Failed("id不能小于等于0");
            return await Task.FromResult(await _tagSvc.GetAsync(id));
        }

        /// <summary>
        /// 获取文章标签列表
        /// </summary>
        [HttpGet("all")]
        public async Task<ServiceResult<List<TagTotalDto>>> GetsAsync()
        {
            return await Task.FromResult(await _tagSvc.GetsAsync());
        }
    }
}
