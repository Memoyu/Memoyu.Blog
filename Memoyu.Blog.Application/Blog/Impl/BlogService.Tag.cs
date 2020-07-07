/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 16:23:15
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询标签列表（已被引用的标签以及引用数）
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync()
        {
            return await _blogCacheService.QueryTagsAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryTagDto>>();

                var tags = from tag in await _tagRepository.GetListAsync()
                           join postTags in await _postTagRepository.GetListAsync() on tag.Id equals postTags.TagId
                           group tag by new
                           {
                               tag.TagName,
                               tag.DisplayName
                           } into g
                           select new QueryTagDto
                           {
                               TagName = g.Key.TagName,
                               DisplayName = g.Key.DisplayName,
                               Count = g.Count()
                           };
                result.IsSuccess(tags);
                return result;
            });
        }
        /// <summary>
        /// 获取标签名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetTagAsync(string name)
        {
            return await _blogCacheService.GetTagAsync(name, async () =>
            {
                var result = new ServiceResult<string>();
                var tag = await _tagRepository.FindAsync(t => t.DisplayName.Equals(name));
                if (null == tag)
                {
                    result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("标签", name));
                    return result;
                }
                result.IsSuccess(tag.TagName);
                return result;
            });
        }
    }
}
