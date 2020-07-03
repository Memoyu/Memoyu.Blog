/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：CategoryRepository
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 17:20:11
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Memoyu.Blog.Domain.Blog;
using Memoyu.Blog.Domain.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Memoyu.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class CategoryRepository : EfCoreRepository<MemoyuBlogDbContext, Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<MemoyuBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
