/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog
*   文件名称 ：MemoyuBlogMigrationsDbContext
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 23:25:15
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Memoyu.Blog
{
    /// <summary>
    /// 数据迁移上下文访问对象
    /// </summary>
    public class MemoyuBlogMigrationsDbContext:AbpDbContext<MemoyuBlogMigrationsDbContext>
    {
        public MemoyuBlogMigrationsDbContext(DbContextOptions<MemoyuBlogMigrationsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configure();
        }
    }
}
