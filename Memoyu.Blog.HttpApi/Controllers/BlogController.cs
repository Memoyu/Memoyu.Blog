/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogController
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/28 16:35:55
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Memoyu.Blog.Application.Blog;
using Memoyu.Blog.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Memoyu.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = MemoyuBlogConsts.Grouping.GroupName_v1)]
    public partial class BlogController : AbpController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #region Posts

        ///// <summary>
        ///// 根据URL获取文章详情
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("post")]
        //public async Task<ServiceResult<PostDetailDto>> GetPostDetailAsync([Required] string url)
        //{
        //    return await _blogService.GetPostDetailAsync(url);
        //}

        ///// <summary>
        ///// 分页查询文章列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("posts")]
        //public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync([FromQuery] PagingInput input)
        //{
        //    return await _blogService.QueryPostsAsync(input);
        //}

        ///// <summary>
        ///// 通过分类名称查询文章列表
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("posts/category")]
        //public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByCategoryAsync([Required] string name)
        //{
        //    return await _blogService.QueryPostsByCategoryAsync(name);
        //}

        ///// <summary>
        ///// 通过标签名称查询文章列表
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("posts/tag")]
        //public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByTagAsync(string name)
        //{
        //    return await _blogService.QueryPostsByTagAsync(name);
        //}

        #endregion Posts

        #region Categories

        ///// <summary>
        ///// 获取分类名称
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("category")]
        //public async Task<ServiceResult<string>> GetCategoryAsync([Required] string name)
        //{
        //    return await _blogService.GetCategoryAsync(name);
        //}

        ///// <summary>
        ///// 查询分类列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("categories")]
        //public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync()
        //{
        //    return await _blogService.QueryCategoriesAsync();
        //}

        #endregion Categories

        #region Tags

        ///// <summary>
        ///// 获取标签名称
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("tag")]
        //public async Task<ServiceResult<string>> GetTagAsync([Required] string name)
        //{
        //    return await _blogService.GetTagAsync(name);
        //}

        ///// <summary>
        ///// 查询标签列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("tags")]
        //public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync()
        //{
        //    return await _blogService.QueryTagsAsync();
        //}

        #endregion Tags

        #region FriendLinks

        ///// <summary>
        ///// 查询友链列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("friendlinks")]
        //public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync()
        //{
        //    return await _blogService.QueryFriendLinksAsync();
        //}

        #endregion FriendLinks
    }
}