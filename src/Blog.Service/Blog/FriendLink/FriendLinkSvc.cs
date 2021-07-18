using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Service.Base;
using Blog.Service.Blog.FriendLink.Output;

namespace Blog.Service.Blog.FriendLink
{
    public class FriendLinkSvc : ApplicationSvc, IFriendLinkSvc
    {
        private readonly IFriendLinkRepo _friendLinkRepo;

        public FriendLinkSvc(IFriendLinkRepo friendLinkRepo)
        {
            _friendLinkRepo = friendLinkRepo;
        }

        public async Task<ServiceResult<List<FriendLinkDto>>> GetsAsync()
        {
            var friendLinks = await _friendLinkRepo
                .Select
                .Where(f => !f.IsDeleted)
                .ToListAsync<FriendLinkDto>();
            return await Task.FromResult(ServiceResult<List<FriendLinkDto>>.Successed(friendLinks));
        }

        public async Task<ServiceResult<FriendLinkDto>> GetAsync(long id)
        {
            var friendLinks = await _friendLinkRepo
                .Select
                .Where(f => f.Id == id)
                .Where(f => !f.IsDeleted)
                .ToOneAsync<FriendLinkDto>();
            return await Task.FromResult(ServiceResult<FriendLinkDto>.Successed(friendLinks));
        }

        public async Task<ServiceResult<List<FriendLinkDto>>> GetByTypeAsync(int type)
        {
            var friendLinks = await _friendLinkRepo
                .Select
                .Where(f => !f.IsDeleted)
                .Where(f => f.Type == type)
                .ToListAsync<FriendLinkDto>();
            return await Task.FromResult(ServiceResult<List<FriendLinkDto>>.Successed(friendLinks));
        }
    }
}
