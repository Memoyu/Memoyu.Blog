/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching.Blog.Impl
*   文件名称 ：BlogCacheService
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 23:18:44
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
using Memoyu.Blog.ToolKits.Extensions;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string KEY_QueryPosts = "Blog:Post:QueryPosts-{0}-{1}";
        private const string KEY_GetPostDetail = "Blog:Post:GetPostDetail-{0}";
        private const string KEY_GetPostsByCategory = "Blog:Post:GetPostsByCategory-{0}-{1}-{2}";
        private const string KEY_GetPostsByTag = "Blog:Post:GetPostsByTag-{0}-{1}-{2}";
        /// <summary>
        /// 分页查询文章列表（缓存）
        /// </summary>
        /// <param name="input">分页入参</param>
        /// <param name="factory">获取最新数据委托</param>
        /// <returns></returns>
        public Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync(PagingInput input, Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory)
        {
            return Cache.GetOrAddAsync(KEY_QueryPosts.FormatWith(input.Page, input.Limit), factory, CacheStrategy.ONE_DAY);
        }
        /// <summary>
        /// 通过分类Id获取文章列表（缓存）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsByCategoryAsync(PagingInputById input, Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetPostsByCategory.FormatWith(input.Id, input.Page, input.Limit), factory, CacheStrategy.ONE_DAY);
        }
        /// <summary>
        /// 通过标签Id获取文章列表（缓存）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsByTagAsync(PagingInputById input, Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetPostsByTag.FormatWith(input.Id, input.Page, input.Limit), factory, CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 文章详情（缓存）
        /// </summary>
        /// <param name="url">文章地址</param>
        /// <param name="factory">获取文章详情委托</param>
        /// <returns></returns>
        public Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url, Func<Task<ServiceResult<PostDetailDto>>> factory)
        {
            return Cache.GetOrAddAsync(KEY_GetPostDetail.FormatWith(url), factory, CacheStrategy.ONE_DAY);
        }
    }
}
