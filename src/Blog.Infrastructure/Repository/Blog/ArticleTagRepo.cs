using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Core.Security;
using Blog.Infrastructure.Repository.Base;
using FreeSql;

namespace Blog.Infrastructure.Repository.Blog
{
    public class ArticleTagRepo : AuditBaseRepo<ArticleTagEntity>, IArticleTagRepo
    {
        public ArticleTagRepo(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task BulkInsertAsync(IEnumerable<ArticleTagEntity> entities)
        {
        }
    }
}
