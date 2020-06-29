using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.Application
{
    /// <summary>
    /// 为我们的应用服务层，在这里编写服务的接口以及对应的实现
    /// </summary>
    [DependsOn(
        typeof(AbpIdentityApplicationModule)
    )]
    public class MemoyuBlogApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
         
        }
    }
}
