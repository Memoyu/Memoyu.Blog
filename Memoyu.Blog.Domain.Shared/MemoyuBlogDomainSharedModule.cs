using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.Domain.Shared
{
    /// <summary>
    /// 层相当于.Domain的一个扩展一样，这里放一下项目用到的枚举、公共常量等内容
    /// </summary>
    [DependsOn(

        typeof(AbpIdentityDomainSharedModule)
    )]
    public class MemoyuBlogDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
