/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching.Blog.Impl
*   文件名称 ：BlogCacheService
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 23:19:03
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        public const string KEY_QueryTags = "Blog:Tag:QueryTags";
        public const string KEY_GetTag = "Blog:Tag:GetTag-{0}";
        /// <summary>
        /// 查询分类列表（已被引用的分类以及引用数）（缓存）
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync(Func<Task<ServiceResult<IEnumerable<QueryTagDto>>>> factory)
        {
            return Cache.GetOrAddAsync(KEY_QueryTags, factory, CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 获取标签名称（缓存）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public Task<ServiceResult<string>> GetTagAsync(string name, Func<Task<ServiceResult<string>>> factory)
        {
            return Cache.GetOrAddAsync(KEY_GetTag.FormatWith(name), factory, CacheStrategy.ONE_DAY);
        }

    }
}
