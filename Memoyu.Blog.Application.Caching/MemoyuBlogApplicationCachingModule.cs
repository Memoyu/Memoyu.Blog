/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching
*   文件名称 ：MemoyuBlogApplicationCachingModule
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-06-21 12:22:24
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Memoyu.Blog.Domain;
using Memoyu.Blog.Domain.Configurations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Memoyu.Blog.Application.Caching
{
    [DependsOn(typeof(AbpCachingModule), typeof(MemoyuBlogDomainModule))]
    public class MemoyuBlogApplicationCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppSettings.Caching.RedisConnectionString;
                });

            var csredis = new CSRedis.CSRedisClient(AppSettings.Caching.RedisConnectionString);
            RedisHelper.Initialization(csredis);
            context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
        }
    }
}
