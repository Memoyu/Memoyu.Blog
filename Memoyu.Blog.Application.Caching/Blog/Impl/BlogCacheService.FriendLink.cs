/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching.Blog.Impl
*   文件名称 ：BlogCacheService
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 23:19:37
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
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        public const string KEY_QueryFriendLinks = "Blog:FriendLink:QueryFriendLinks";
        /// <summary>
        /// 查询友链列表（缓存）
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync(Func<Task<ServiceResult<IEnumerable<FriendLinkDto>>>>factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryFriendLinks, factory, CacheStrategy.ONE_DAY);
        }
    }
}
