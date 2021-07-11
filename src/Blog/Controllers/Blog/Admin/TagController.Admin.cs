using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Service.Blog.Tag;
using Blog.Service.Blog.Tag.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog.Admin
{
    [ApiController]
    [Route("api/admin/tag")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v2)]
    [Authorize]
    public class TagController
    {
        private readonly ITagSvc _tagSvc;

        public TagController(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        /// <summary>
        /// 新增文章标签
        /// </summary>
        /// <param name="add">文章标签信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult> AddAsync([FromBody] AddTagDto add)
        {
            var validation = add.Validation();
            if (validation.Fail) return ServiceResult.Failed(validation.Msg);
            return await Task.FromResult(await _tagSvc.AddAsync(add));
        }

        /// <summary>
        /// 删除文章标签
        /// </summary>
        /// <param name="id">文章标签Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ServiceResult> DeleteAsync(long id)
        {
            if (id <= 0)
                return ServiceResult.Failed("id不能小于等于0");
            return await Task.FromResult(await _tagSvc.DeleteAsync(id));
        }

        /// <summary>
        /// 更新文章标签
        /// </summary>
        /// <param name="modify">文章标签信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ServiceResult> UpdateAsync([FromBody] ModifyTagDto modify)
        {
            var validation = modify.Validation();
            if (validation.Fail) return ServiceResult.Failed(validation.Msg);
            return await Task.FromResult(await _tagSvc.UpdateAsync(modify));
        }
    }
}
