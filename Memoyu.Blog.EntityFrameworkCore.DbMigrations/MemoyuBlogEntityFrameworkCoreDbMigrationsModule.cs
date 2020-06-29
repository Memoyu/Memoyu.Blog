/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog
*   文件名称 ：MemoyuBlogEntityFrameworkCoreDbMigrationsModule
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 23:23:15
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.EntityFrameworkCore.DbMigrations
{
    [DependsOn(typeof(MemoyuBlogFrameworkCoreModule))]
    public class MemoyuBlogEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MemoyuBlogMigrationsDbContext>();
        }
    }
}
