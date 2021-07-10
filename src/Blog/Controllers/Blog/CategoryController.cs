using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Service.Blog.Category;
using Blog.Service.Blog.Category.Input;
using Blog.Service.Blog.Category.Output;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog
{
    /// <summary>
    /// 文章分类
    /// </summary>
    [ApiController]
    [Route("api/category")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v1)]
    public class CategoryController
    {
        private readonly ICategorySvc _categorySvc;

        public CategoryController(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        /// <summary>
        /// 获取文章分类
        /// </summary>
        /// <param name="id">文章分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<CategoryTotalDto>> GetAsync([FromQuery] long id)
        {
            if (id <= 0)
                return ServiceResult<CategoryTotalDto>.Failed("id不能小于等于0");
            return await Task.FromResult(await _categorySvc.GetAsync(id));
        }

        /// <summary>
        /// 获取文章分类列表
        /// </summary>
        [HttpGet("all")]
        public async Task<ServiceResult<List<CategoryTotalDto>>> GetsAsync()
        {
            return await Task.FromResult(await _categorySvc.GetsAsync());
        }
    }
}
