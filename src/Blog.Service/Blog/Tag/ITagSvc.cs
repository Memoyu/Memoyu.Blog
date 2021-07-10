using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Service.Blog.Tag.Input;
using Blog.Service.Blog.Tag.Output;

namespace Blog.Service.Blog.Tag
{
    public interface ITagSvc
    {
        /// <summary>
        /// 新增文章标签
        /// </summary>
        /// <param name="add">文章标签信息</param>
        /// <returns></returns>
        Task<ServiceResult> AddAsync(AddTagDto add);

        /// <summary>
        /// 删除文章标签
        /// </summary>
        /// <param name="id">文章标签Id</param>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsync(long id);

        /// <summary>
        /// 获取文章标签
        /// </summary>
        /// <param name="id">文章标签Id</param>
        /// <returns></returns>
        Task<ServiceResult<TagTotalDto>> GetAsync(long id);

        /// <summary>
        /// 获取所有文章标签列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<List<TagTotalDto>>> GetsAsync();

        /// <summary>
        /// 更新文章标签
        /// </summary>
        /// <param name="modify">文章标签信息</param>
        /// <returns></returns>
        Task<ServiceResult> UpdateAsync(ModifyTagDto modify);
    }
}
