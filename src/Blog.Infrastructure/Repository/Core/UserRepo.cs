using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Core;
using Blog.Core.Security;
using Blog.Infrastructure.Repository.Base;
using FreeSql;

namespace Blog.Infrastructure.Repository.Core
{
    public class UserRepo : AuditBaseRepo<UserEntity>, IUserRepo
    {
        public UserRepo(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {

        }

        public Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> expression)
        {
            return Select.Where(expression).ToOneAsync();
        }

        public async Task<UserEntity> AddOrUpdateAsync(UserEntity entity)
        {
            var user = await GetAsync(s => s.UserId == entity.UserId && !s.IsDeleted);
            if (user != null)
            {
                entity.Id = user.Id;
                Expression<Func<UserEntity, object>> ignoreExp = e => new { e.CreateUserId, e.CreateTime };
                await UpdateWithIgnoreAsync(entity, ignoreExp);
            }
            else
            {
                await InsertAsync(entity);
            }

            return entity;
        }
    }
}
