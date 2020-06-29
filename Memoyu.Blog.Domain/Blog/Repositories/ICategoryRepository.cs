/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：ICategoryRepository
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/28 16:53:38
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using Volo.Abp.Domain.Repositories;

namespace Memoyu.Blog.Domain.Blog.Repositories
{
    /// <summary>
    /// ICategoryRepository
    /// </summary>
    public interface ICategoryRepository : IRepository<Category, int>
    {
    }
}
