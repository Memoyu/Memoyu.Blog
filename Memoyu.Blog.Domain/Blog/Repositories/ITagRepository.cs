/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：ITagRepository
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/28 16:54:39
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Memoyu.Blog.Domain.Blog.Repositories
{
    /// <summary>
    /// ITagRepository
    /// </summary>
    public interface ITagRepository:IRepository<Tag , int>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<Tag> tags);
    }
}
