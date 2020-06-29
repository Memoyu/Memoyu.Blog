/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：IFriendLinkRepository
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/6/28 16:56:53
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
    /// IFriendLinkRepository
    /// </summary>
    public interface IFriendLinkRepository:IRepository<FriendLink , int>
    {
    }
}
