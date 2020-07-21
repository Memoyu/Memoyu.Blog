/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching.Blog
*   文件名称 ：IBlogCacheService
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 23:15:56
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
using Memoyu.Blog.Application.Contracts;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.ToolKits.Base;

namespace Memoyu.Blog.Application.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        /// <summary>
        /// 分页查询文章列表（缓存）
        /// </summary>
        /// <param name="input">分页入参</param>
        /// <param name="factory">获取最新数据委托</param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync(PagingInput input,
            Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory);
        /// <summary>
        /// 通过分类Id获取文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsByCategoryAsync(PagingInputById input, Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory);
        /// <summary>
        /// 通过标签Id获取文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsByTagAsync(PagingInputById input, Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory);
        /// <summary>
        /// 文章详情（缓存）
        /// </summary>
        /// <param name="url">文章地址</param>
        /// <param name="factory">获取文章详情委托</param>
        /// <returns></returns>
        Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url,
            Func<Task<ServiceResult<PostDetailDto>>> factory);

    }
}
