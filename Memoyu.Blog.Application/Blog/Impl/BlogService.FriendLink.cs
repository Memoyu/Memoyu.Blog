/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：BlogService
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2020/7/3 16:24:15
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.Domain.Blog;
using Memoyu.Blog.ToolKits.Base;

namespace Memoyu.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询友链列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync()
        {
            return await _blogCacheService.QueryFriendLinksAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<FriendLinkDto>>();
                var list = await _friendLinksRepository.GetListAsync();
                var friendLinks = ObjectMapper.Map<IEnumerable<FriendLink>, IEnumerable<FriendLinkDto>>(list);
                result.IsSuccess(friendLinks);
                return result;
            });
        }
    }
}
