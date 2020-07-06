/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/28 19:28:20
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Linq;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;

namespace Memoyu.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 根据Url获取文章详情
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns></returns>
        public Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url)
        {
            throw new Exception("这是个异常测试");
        }
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input">分页入参</param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync(PagingInput input)
        {
            return await _blogCacheService.QueryPostsAsync(input, async () =>
            {
                var result = new ServiceResult<PagedList<QueryPostDto>>();
                var count = await _postRepository.GetCountAsync();
                var list = _postRepository
                    .OrderByDescending(p => p.CreationTime)
                    .PageByIndex(input.Page, input.Limit)
                    .Select(p => new PostBriefDto//排序后，转成列表摘要，
                    {
                        Title = p.Title,
                        Url = p.Url,
                        Year = p.CreationTime.Year,
                        CreationTime = p.CreationTime.TryToDateTime()
                    })
                    .GroupBy(p => p.Year)
                    .Select(p => new QueryPostDto//分组后，转成分组列表
                    {
                        Year = p.Key,
                        Posts = p.ToList()
                    }).ToList();

                result.IsSuccess(new PagedList<QueryPostDto>(count.TryToInt(), list));
                return result;
            });
        }
    }
}
