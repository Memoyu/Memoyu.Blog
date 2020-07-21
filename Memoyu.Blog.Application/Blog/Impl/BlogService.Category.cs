/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 16:23:46
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.ToolKits.Base;
using Memoyu.Blog.ToolKits.Extensions;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询分类列表（已被引用的分类以及引用数）
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync()
        {
            return await _blogCacheService.QueryCategoriesAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryCategoryDto>>();

                var categories = from category in await _categoryRepository.GetListAsync()
                                 join posts in await _postRepository.GetListAsync()
                                     on category.Id equals posts.CategoryId
                                 group category by new
                                 {
                                     category.CategoryName,
                                     category.DisplayName,
                                     category.Id
                                 } into g
                                 select new QueryCategoryDto
                                 {
                                     Id = g.Key.Id,
                                     CategoryName = g.Key.CategoryName,
                                     DisplayName = g.Key.DisplayName,
                                     Count = g.Count()
                                 };
                result.IsSuccess(categories);
                return result;
            });
        }
        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetCategoryAsync(string name)
        {
            return await _blogCacheService.GetCategoryAsync(name, async () =>
            {
                var result = new ServiceResult<string>();
                var category = await _categoryRepository.FindAsync(c => c.DisplayName.Equals(name));//获取分类
                if (null == category)//空则报错
                {
                    result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("分类", name));
                    return result;
                }
                result.IsSuccess(category.CategoryName);
                return result;
            });
        }
    }
}
