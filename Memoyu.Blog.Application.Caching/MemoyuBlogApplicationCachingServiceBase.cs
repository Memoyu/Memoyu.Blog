/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application.Caching
*   文件名称 ：MemoyuBlogApplicationCachingServiceBase
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-04 20:56:35
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memoyu.Blog.ToolKits.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.DependencyInjection;

namespace Memoyu.Blog.Application.Caching
{
    public class CachingServiceBase : ITransientDependency
    {
        public IDistributedCache Cache { get; set; }
        public async Task RemoveAsync(string key, int cursor = 0)
        {
            var scan = await RedisHelper.ScanAsync(cursor);//获取Redis中所有的keys
            var keys = scan.Items;

            if (keys.Any() && key.IsNotNullOrEmpty())
            {
                keys = keys.Where(x => x.StartsWith(key)).ToArray();//遍历keys集合，取出符合条件的key
                await RedisHelper.DelAsync(keys);//删除数据
            }
        }

    }
    public interface ICacheRemoveService
    {
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        Task RemoveAsync(string key, int cursor = 0);
    }
}
