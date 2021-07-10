using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Service.Blog.Category.Input;
using Blog.Service.Blog.Category.Output;

namespace Blog.Service.Blog.Category
{
    public interface ICategorySvc
    {
        /// <summary>
        /// 新增文章分类
        /// </summary>
        /// <param name="add">文章分类信息</param>
        /// <returns></returns>
        Task<ServiceResult>AddAsync(AddCategoryDto add);

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">文章分类Id</param>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsync(long id);

        /// <summary>
        /// 获取文章分类
        /// </summary>
        /// <param name="id">文章分类Id</param>
        /// <returns></returns>
        Task<ServiceResult<CategoryTotalDto>> GetAsync(long id);

        /// <summary>
        /// 获取所有文章分类列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<List<CategoryTotalDto>>> GetsAsync();

        /// <summary>
        /// 更新文章分类
        /// </summary>
        /// <param name="modify">文章分类信息</param>
        /// <returns></returns>
        Task<ServiceResult> UpdateAsync(ModifyCategoryDto modify);
    }
}
