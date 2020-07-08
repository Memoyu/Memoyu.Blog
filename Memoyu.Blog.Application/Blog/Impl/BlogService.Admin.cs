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
        public async Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id)
        {
            var result = new ServiceResult<PostForAdminDto>();
            var post = await _postRepository.GetAsync(id);
            var tags = from postTag in await _postTagRepository.GetListAsync()
                       join tag in await _tagRepository.GetListAsync()
                       on postTag.TagId equals tag.Id
                       where postTag.PostId.Equals(post.Id)
                       select tag.TagName;
            var detail = ObjectMapper.Map<Post, PostForAdminDto>(post);
            detail.Tags = tags;
            detail.Url = post.Url.Split("/").Last(x => !string.IsNullOrEmpty(x));

            result.IsSuccess(detail);
            return result;
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
            await _blogCacheService.RemoveAsync(CachePrefix.BlogPost);// 执行清除缓存操作
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
            var oldPostTags = from postTag in await _postTagRepository.GetListAsync()//查出文章中现有的标签数据
                              join tag in await _tagRepository.GetListAsync()
                                  on postTag.TagId equals tag.Id
                              where postTag.PostId.Equals(post.Id)
                              select new
                              {
                                  postTag.Id,
                                  tag.TagName
                              };
            var removeTag = oldPostTags.Where(//获取需要移除的
                    item => input.Tags.All(it => it != item.TagName) &&//All 当全部元素符合条件才返回True
                    tags.Any(t => t.TagName == item.TagName))
                    .Select(item => item.Id);
            await _postTagRepository.DeleteAsync(pt => removeTag.Contains(pt.Id));//删除文章中多余的Tag

            var newTags = input.Tags//获取传入input中新的tags
                .Where(it => !tags.Any(t => t.TagName.Equals(it)))
                .Select(it => new Tag
                {
                    TagName = it,
                    DisplayName = it
                });
            await _tagRepository.BulkInsertAsync(newTags);

            var newPostTags = input.Tags//获取需要新增的PostTag
                .Where(it => !oldPostTags.Any(ot => ot.TagName.Equals(it)))
                .Select(it =>
                new PostTag
                {
                    PostId = id,
                    TagId = _tagRepository.FirstOrDefault(t => t.TagName.Equals(it)).Id
                });
            await _postTagRepository.BulkInsertAsync(newPostTags);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogPost);// 执行清除缓存操作
            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            var result = new ServiceResult();

            var post = await _postRepository.GetAsync(id);
            if (null == post)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }

            await _postRepository.DeleteAsync(id);
            await _postTagRepository.DeleteAsync(x => x.PostId == id);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogPost);// 执行清除缓存操作
            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }

        #endregion

        #region Categories

        /// <summary>
        /// 查询分类列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdminAsync()
        {
            var result = new ServiceResult<IEnumerable<QueryCategoryForAdminDto>>();

            var posts = await _postRepository.GetListAsync();
            var categories = (await _categoryRepository.GetListAsync()).Select(c => new QueryCategoryForAdminDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                DisplayName = c.DisplayName,
                Count = posts.Count(p => p.CategoryId == c.Id)
            });
            result.IsSuccess(categories);
            return result;
        }
        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertCategoryAsync(EditCategoryInput input)
        {
            var result = new ServiceResult();
            var category = ObjectMapper.Map<EditCategoryInput, Category>(input);
            await _categoryRepository.InsertAsync(category);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogCategory);// 执行清除缓存操作
            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }
        /// <summary>
        /// 更新分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateCategoryAsync(int id, EditCategoryInput input)
        {
            var result = new ServiceResult();
            var category = await _categoryRepository.GetAsync(id);
            category.CategoryName = input.CategoryName;
            category.DisplayName = input.DisplayName;
            await _categoryRepository.UpdateAsync(category);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogCategory);
            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResult();
            var category = await _categoryRepository.GetAsync(id);
            if (null == category)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }
            await _categoryRepository.DeleteAsync(id);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogCategory);
            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }

        #endregion Categories

        #region Tags

        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>> QueryTagsForAdminAsync()
        {
            var result = new ServiceResult<IEnumerable<QueryTagForAdminDto>>();

            var postTags = await _postTagRepository.GetListAsync();
            var tags = (await _tagRepository.GetListAsync()).Select(t => new QueryTagForAdminDto
            {
                Id = t.Id,
                TagName = t.TagName,
                DisplayName = t.DisplayName,
                Count = postTags.Count(pt => pt.TagId == t.Id)
            });

            result.IsSuccess(tags);
            return result;
        }
        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertTagAsync(EditTagInput input)
        {
            var result = new ServiceResult();
            var tag = ObjectMapper.Map<EditTagInput, Tag>(input);
            await _tagRepository.InsertAsync(tag);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogTag);//移除缓存
            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }
        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateTagAsync(int id, EditTagInput input)
        {
            var result = new ServiceResult();
            var tag = await _tagRepository.GetAsync(id);
            tag.TagName = input.TagName;
            tag.DisplayName = input.DisplayName;
            await _tagRepository.UpdateAsync(tag);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogTag);
            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteTagAsync(int id)
        {
            var result = new ServiceResult();
            var tag = await _tagRepository.GetAsync(id);
            if (null == tag)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }
            await _tagRepository.DeleteAsync(id);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogTag);
            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }

        #endregion Tags

        #region FriendLink

        /// <summary>
        /// 查询友链列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinksForAdminAsync()
        {
            var result = new ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>();
            var friendLinks = await _friendLinksRepository.GetListAsync();
            var dto = ObjectMapper.Map<List<FriendLink>, IEnumerable<QueryFriendLinkForAdminDto>>(friendLinks);

            result.IsSuccess(dto);
            return result;
        }
        /// <summary>
        /// 新增友链
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertFriendLinkAsync(EditFriendLinkInput input)
        {
            var result = new ServiceResult();
            var friendLink = ObjectMapper.Map<EditFriendLinkInput, FriendLink>(input);
            await _friendLinksRepository.InsertAsync(friendLink);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogFriendLink);// 执行清除缓存操作
            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }
        /// <summary>
        /// 更新友链
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateFriendLinkAsync(int id, EditFriendLinkInput input)
        {
            var result = new ServiceResult();
            var friendLink = await _friendLinksRepository.GetAsync(id);
            friendLink.Title = input.Title;
            friendLink.LinkUrl = input.LinkUrl;
            await _friendLinksRepository.UpdateAsync(friendLink);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogFriendLink);
            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }
        /// <summary>
        /// 删除友链
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteFriendLinkAsync(int id)
        {
            var result = new ServiceResult();
            var friendLink = await _friendLinksRepository.GetAsync(id);
            if (null == friendLink)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }
            await _friendLinksRepository.DeleteAsync(id);
            await _blogCacheService.RemoveAsync(CachePrefix.BlogFriendLink);
            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }

        #endregion FriendLink
    }
}
