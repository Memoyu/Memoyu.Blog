/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching.Blog.Impl
*   文件名称 ：BlogCacheService
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 23:17:45
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.Domain.Shared;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string KEY_QueryCategories = "Blog:Category:QueryCategories";
        private const string KEY_GetCategory = "Blog:Category:GetCategory-{0}";
        /// <summary>
        /// 查询分类列表（已被引用的分类以及引用数）（缓存）
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync(Func<Task<ServiceResult<IEnumerable<QueryCategoryDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryCategories, factory, CacheStrategy.ONE_DAY);
        }
        /// <summary>
        /// 获取分类名称（缓存）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetCategoryAsync(string name ,Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetCategory.FormatWith(name), factory, CacheStrategy.ONE_DAY);
        }
    }
}
