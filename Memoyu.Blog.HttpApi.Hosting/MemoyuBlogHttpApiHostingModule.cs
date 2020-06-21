using Memoyu.Blog.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.HttpApi.Hosting
{
    /// <summary>
    /// 相当于一个web项目，但这里主要依赖于HttpApi模块，用来暴露我们的API的。
    /// </summary>
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(MomeyuBlogHttpApiModule),
        typeof(MemoyuBlogSwaggerModule),
        typeof(MemoyuBlogFrameworkCoreModule)
        )]
    public class MemoyuBlogHttpApiHostingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            //判断环境变量，开发环境，
            if (env.IsDevelopment())
            {
                //生成异常页面
                app.UseDeveloperExceptionPage();
            }
            //路由
            app.UseRouting();
            //路由映射
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
