using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Blog
{
    /// <summary>
    /// 文章标签Repo
    /// </summary>
    public interface ITagRepo : IAuditBaseRepo<TagEntity>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<TagEntity> entities);
    }
}
