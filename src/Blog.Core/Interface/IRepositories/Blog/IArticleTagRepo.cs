using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Blog
{
    /// <summary>
    /// 文章与标签关系Repo
    /// </summary>
    public interface IArticleTagRepo : IAuditBaseRepo<ArticleTagEntity>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<ArticleTagEntity> entities);
    }
}
