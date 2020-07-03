/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：TagRepository
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 17:21:40
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using Memoyu.Blog.Domain.Blog;
using Memoyu.Blog.Domain.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Memoyu.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class TagRepository : EfCoreRepository<MemoyuBlogDbContext, Tag, int>, ITagRepository
    {
        public TagRepository(IDbContextProvider<MemoyuBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task BulkInsertAsync(IEnumerable<Tag> tags)
        {
            await DbContext.Set<Tag>().AddRangeAsync(tags);
            await DbContext.SaveChangesAsync();
        }
    }
}
