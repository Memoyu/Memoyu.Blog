using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Core;
using Blog.Core.Security;
using Blog.Infrastructure.Repository.Base;
using FreeSql;

namespace Blog.Infrastructure.Repository.Core
{
    public class BaseItemRepo : AuditBaseRepo<BaseItemEntity>, IBaseItemRepo
    {
        public BaseItemRepo(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}
