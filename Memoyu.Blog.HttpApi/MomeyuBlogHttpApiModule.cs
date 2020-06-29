using Memoyu.Blog.Application;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.HttpApi
{
    /// <summary>
    /// 职责主要是编写Controller，所有的API都写在这里，同时它要依赖于Application模块
    /// </summary>
    [DependsOn(
        typeof(AbpIdentityHttpApiModule),
        typeof(MemoyuBlogApplicationModule)
        )]
    public class MomeyuBlogHttpApiModule : AbpModule
    {
        
    }
}
