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
using Memoyu.Blog.Domain.Shared;
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
            return _blogCacheService.GetPostDetailAsync(url, async () =>
            {
                var result = new ServiceResult<PostDetailDto>();
                var post = await _postRepository.FindAsync(p => p.Url.Equals(url));//获取url对应的Post
                if (post == null)
                {
                    result.IsFailed(MemoyuBlogConsts.ResponseText.WHAT_NOT_EXIST.FormatWith("URL", url));
                    return result;
                }

                var category = await _categoryRepository.GetAsync(post.CategoryId);//获取Post所属Category
                var tags = from postTags in await _postTagRepository.GetListAsync()//获取Post所属Tag
                           join tag in _tagRepository on postTags.TagId equals tag.Id
                           where postTags.PostId.Equals(post.Id)
                           select new TagDto
                           {
                               DisplayName = tag.DisplayName,
                               TagName = tag.TagName
                           };
                var previous = _postRepository.Where(p => DateTime.Compare(post.CreationTime, p.CreationTime) < 0)//t1早于t2
                    .Take(1).FirstOrDefault();
                var next = _postRepository.Where(p => DateTime.Compare(post.CreationTime, p.CreationTime) > 0)//t1晚于t2
                    .OrderByDescending(p=>p.CreationTime).Take(1).FirstOrDefault();
                var postDetail = new PostDetailDto
                {
                    Title = post.Title,
                    Author = post.Author,
                    Url = post.Url,
                    Html = post.Html,
                    Markdown = post.Markdown,
                    CreationTime = post.CreationTime.TryToDateTime(),
                    Category = new CategoryDto
                    {
                        CategoryName = category.CategoryName,
                        DisplayName = category.DisplayName
                    },
                    Tags = tags,
                    Previous = previous == null
                        ? null
                        : new PostForPagedDto
                        {
                            Title = previous.Title,
                            Url = previous.Url
                        },
                    Next = next == null
                        ? null
                        : new PostForPagedDto
                        {
                            Title = next.Title,
                            Url = next.Url
                        }
                };
                result.IsSuccess(postDetail);
                return result;
            });
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
