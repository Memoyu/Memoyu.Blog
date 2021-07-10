using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Blog
{
    /// <summary>
    /// 友链Repo
    /// </summary>
    public interface IFriendLinkRepo : IAuditBaseRepo<FriendLinkEntity>
    {
    }
}
