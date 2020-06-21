/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog
*   文件名称 ：MemoyuBlogFrameworkCoreModule
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 18:17:53
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
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Memoyu.Blog
{
    /// <summary>
    /// 它主要是集成了EF Core，自定义仓储
    /// </summary>
    [DependsOn(
        typeof(MemoyuBlogDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule))]
    public class MemoyuBlogFrameworkCoreModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
