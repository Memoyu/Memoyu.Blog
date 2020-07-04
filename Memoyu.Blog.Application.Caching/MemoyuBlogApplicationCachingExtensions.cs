/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching
*   文件名称 ：MemoyuBlogApplicationCachingExtensions
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 15:15:59
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
using System.Threading.Tasks;
using Memoyu.Blog.ToolKits.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using static Memoyu.Blog.Domain.Shared.MemoyuBlogConsts;

namespace Memoyu.Blog.Application.Caching
{
    public static class MemoyuBlogApplicationCachingExtensions
    {
        public static async Task<TCacheItem> GetOrAddAsync<TCacheItem>(this IDistributedCache cache, string key,
            Func<Task<TCacheItem>> factory, int minutes)
        {
            TCacheItem cacheItem;
            var result = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(result))//缓存是否为空
            {
                cacheItem = await factory.Invoke();
                var options = new DistributedCacheEntryOptions();//缓存配置
                if (minutes != CacheStrategy.NEVER)//缓存超时为NEVER时，
                {
                    options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutes);//配置缓存的到期时间
                }
                await cache.SetStringAsync(key, cacheItem.ToJson(), options);//写入缓存数据
            }
            else
            {
                cacheItem = result.FromJson<TCacheItem>();//Json转模型
            }

            return cacheItem;
        }
    }
}
