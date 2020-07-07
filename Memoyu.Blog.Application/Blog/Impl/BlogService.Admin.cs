/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 16:22:30
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
using Memoyu.Blog.Application.Contracts;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.Application.Contracts.Blog.Params;
using Memoyu.Blog.Domain.Blog;
using Memoyu.Blog.Domain.Shared;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        #region Posts
        /// <summary>
        /// 获取文章详情（管理员）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input)
        {
            var result = new ServiceResult<PagedList<QueryPostForAdminDto>>();
            var count = await _postRepository.GetCountAsync();
            var posts =
                _postRepository.OrderByDescending(p => p.CreationTime)
                    .PageByIndex(input.Page, input.Limit)
                    .Select(p => new PostBriefForAdminDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Url = p.Url,
                        Year = p.CreationTime.Year,
                        CreationTime = p.CreationTime.TryToDateTime()
                    })
                    .GroupBy(p => p.Year)
                    .Select(p => new QueryPostForAdminDto
                    {
                        Year = p.Key,
                        Posts = p.ToList()
                    }).ToList();
            result.IsSuccess(new PagedList<QueryPostForAdminDto>(count.TryToInt(), posts));
            return result;
        }
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertPostAsync(EditPostInput input)
        {
            var result = new ServiceResult();
            var post = ObjectMapper.Map<EditPostInput, Post>(input);
            post.Url = $"{post.CreationTime.ToString(" yyyy MM dd ").Replace(" ", "/")}{post.Url}/";
            await _postRepository.InsertAsync(post);
            var tags = await _tagRepository.GetListAsync();

            var newTags = input.Tags//找出新标签，进行添加
                .Where(t => !tags
                .Any(a => a.TagName.Equals(t)))
                .Select(t => new Tag
                {
                    DisplayName = t,
                    TagName = t
                }).ToList();
            await _tagRepository.BulkInsertAsync(newTags);

            var postTags = input.Tags//构建文章对应标签数据
                .Select(t => new PostTag
                {
                    PostId = post.Id,
                    TagId = _tagRepository.FirstOrDefault(a => a.TagName.Equals(t)).Id
                });
            await _postTagRepository.BulkInsertAsync(postTags);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        ///  <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdatePostAsync(int id, EditPostInput input)
        {
            var result = new ServiceResult();
            var post = await _postRepository.GetAsync(id);
            post.Title = input.Title;
            post.Author = input.Author;
            post.Url = $"{input.CreationTime.ToString(" yyyy MM dd ").Replace(" ", "/")}{input.Url}/";
            post.Html = input.Html;
            post.Markdown = input.Markdown;
            post.CreationTime = input.CreationTime;
            post.CategoryId = input.CategoryId;

            await _postRepository.UpdateAsync(post);
            var tags = await _tagRepository.GetListAsync();
            var oldPostTags = from postTag in await _postTagRepository.GetListAsync()
                              join tag in await _tagRepository.GetListAsync()
                                  on postTag.TagId equals tag.Id
                              where postTag.PostId.Equals(post.Id)
                              select new
                              {
                                  postTag.Id,
                                  tag.TagName
                              };
            var removeTag =//All 当全部元素符合条件才返回True
                oldPostTags.Where(
                    item => input.Tags.All(it => it != item.TagName) &&
                         tags.Any(t => t.TagName == item.TagName))
                    .Select(item => item.Id);

            return result;
        }

        #endregion
    }
}
