using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Core;
using Blog.Core.Security;
using Blog.Infrastructure.Repository.Base;
using FreeSql;

namespace Blog.Infrastructure.Repository.Core
{
    public class AdminUserRepo : AuditBaseRepo<AdminUserEntity>, IAdminUserRepo
    {
        public AdminUserRepo(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        public async Task<List<AdminUserEntity>> GetsAsync()
        {
            return await Select.Where(a => a.IsDeleted == false && a.IsEnable).ToListAsync();
        }

        public Task UpdateLastLoginTimeAsync(long userId)
        {
            return UpdateDiy.Set(r => new AdminUserEntity
            {
                LastLoginTime = DateTime.Now
            }).Where(r => r.UserId == userId).ExecuteAffrowsAsync();
        }
    }
}
