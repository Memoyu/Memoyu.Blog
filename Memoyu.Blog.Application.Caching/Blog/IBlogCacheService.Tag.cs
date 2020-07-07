/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching.Blog
*   文件名称 ：IBlogCacheService
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 23:16:40
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

namespace Memoyu.Blog.Application.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        /// <summary>
        /// 查询分标签表（已被引用的标签以及引用数）（缓存）
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync(Func<Task<ServiceResult<IEnumerable<QueryTagDto>>>> factory);
        /// <summary>
        /// 获取标签名称（缓存）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        Task<ServiceResult<string>> GetTagAsync(string name, Func<Task<ServiceResult<string>>> factory);
    }
}
