using Memoyu.Blog.Application.Caching;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.Application
{
    /// <summary>
    /// 为我们的应用服务层，在这里编写服务的接口以及对应的实现
    /// </summary>
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(MemoyuBlogApplicationCachingModule)

    )]
    public class MemoyuBlogApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MemoyuBlogAutoMapperProfile>(true);
                options.AddProfile<MemoyuBlogAutoMapperProfile>(true);
            });
        }
    }
}
