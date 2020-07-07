/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：IBlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 16:22:00
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.Application.Contracts.Blog.Params;
using Memoyu.Blog.ToolKits.Base;

namespace Memoyu.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        #region Posts

        /// <summary>
        /// 获取文章详情（管理员）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id);
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input);
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> InsertPostAsync(EditPostInput input);
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdatePostAsync(int id,EditPostInput input);

        #endregion
    }
}
