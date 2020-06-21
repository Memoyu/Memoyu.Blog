using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Memoyu.Blog
{
    [DependsOn(
        typeof(AbpIdentityDomainModule)
    )]
    public class MemoyuBlogDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
           
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }
    }

}
