using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Controllers.Core;
using Blog.Core.Domains.Common;
using Blog.Core.Domains.Common.Consts;
using Blog.Service.Blog.FriendLink;
using Blog.Service.Blog.FriendLink.Output;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.Blog
{
    /// <summary>
    /// 友链
    /// </summary>
    [Route("api/friend-link")]
    [ApiExplorerSettings(GroupName = SystemConst.Grouping.GroupName_v1)]
    public class FriendLinkController : ApiControllerBase
    {
        private readonly IFriendLinkSvc _friendLinkSvc;

        public FriendLinkController(IFriendLinkSvc friendLinkSvc)
        {
            _friendLinkSvc = friendLinkSvc;
        }

        /// <summary>
        /// 获取友链列表
        /// </summary>
        [HttpGet("all")]
        public async Task<ServiceResult<List<FriendLinkDto>>> GetsAsync()
        {
            return await Task.FromResult(await _friendLinkSvc.GetsAsync());
        }

        /// <summary>
        /// 获取友链
        /// </summary>
        /// <param name="id">友链id</param>
        [HttpGet("{id}")]
        public async Task<ServiceResult<FriendLinkDto>> GetAsync(long id)
        {
            return await Task.FromResult(await _friendLinkSvc.GetAsync(id));
        }

        /// <summary>
        /// 获取友链列表(根据类型获取)
        /// </summary>
        [HttpGet("type/{type}")]
        public async Task<ServiceResult<List<FriendLinkDto>>> GetByTypeAsync(int type)
        {
            return await Task.FromResult(await _friendLinkSvc.GetByTypeAsync(type));
        }
    }
}
