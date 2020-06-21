/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Swagger
*   文件名称 ：MemoyuBlogSwaggerModule
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 12:24:24
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Microsoft.AspNetCore.Builder;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.Swagger
{
    [DependsOn(typeof(MemoyuBlogDomainModule))]
    public class MemoyuBlogSwaggerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwagger();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.GetApplicationBuilder().UseSwagger().UseSwaggerUI();
        }
    }
}
