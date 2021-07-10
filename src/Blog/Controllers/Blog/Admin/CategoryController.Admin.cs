using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Service.Blog.Category;
using Blog.Service.Blog.Category.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog.Admin
{
    [ApiController]
    [Route("api/admin/category")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v2)]
    [Authorize]
    public class CategoryController
    {
        private readonly ICategorySvc _categorySvc;

        public CategoryController(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        /// <summary>
        /// 新增文章分类
        /// </summary>
        /// <param name="add">文章分类信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult> AddAsync([FromBody] AddCategoryDto add)
        {
            var validation = add.Validation();
            if (validation.Fail) return ServiceResult.Failed(validation.Msg);
            return await Task.FromResult(await _categorySvc.AddAsync(add));
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">文章分类Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ServiceResult> DeleteAsync([FromQuery] long id)
        {
            if (id <= 0)
                return ServiceResult.Failed("id不能小于等于0");
            return await Task.FromResult(await _categorySvc.DeleteAsync(id));
        }

        /// <summary>
        /// 更新文章分类
        /// </summary>
        /// <param name="modify">文章分类信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ServiceResult> UpdateAsync([FromBody] ModifyCategoryDto modify)
        {
            var validation = modify.Validation();
            if (validation.Fail) return ServiceResult.Failed(validation.Msg);
            return await Task.FromResult(await _categorySvc.UpdateAsync(modify));
        }
    }
}
