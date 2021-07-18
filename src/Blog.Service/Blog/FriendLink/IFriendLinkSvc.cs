using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Service.Blog.FriendLink.Output;

namespace Blog.Service.Blog.FriendLink
{
    public interface IFriendLinkSvc
    {
        /// <summary>
        /// 获取友链列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<List<FriendLinkDto>>> GetsAsync();

        /// <summary>
        /// 获取友链
        /// </summary>
        /// <param name="id">友链id</param>
        /// <returns></returns>
        Task<ServiceResult<FriendLinkDto>> GetAsync(long id);

        /// <summary>
        /// 获取友链列表(根据类型获取)
        /// </summary>
        /// <param name="type">类型：0：友链；1：推荐网站</param>
        /// <returns></returns>
        Task<ServiceResult<List<FriendLinkDto>>> GetByTypeAsync(int type);
    }
}
