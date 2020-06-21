/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog
*   文件名称 ：MemoyuBlogMigrationsDbContextFactory
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 23:25:37
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Memoyu.Blog
{
    /// <summary>
    /// 主要是用来使用Code-First命令的(Add-Migration 和 Update-Database ...)
    /// </summary>
    public class MemoyuBlogMigrationsDbContextFactory:IDesignTimeDbContextFactory<MemoyuBlogMigrationsDbContext>
    {
        public MemoyuBlogMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder =
                new DbContextOptionsBuilder<MemoyuBlogMigrationsDbContext>().UseMySql(
                    configuration.GetConnectionString("Default"));
            return new MemoyuBlogMigrationsDbContext(builder.Options);
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
