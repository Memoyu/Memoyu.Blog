using System;
using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Base;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.Interface.IRepositories.Core
{
    public interface IUserRepo : IAuditBaseRepo<UserEntity>
    {
        /// <summary>
        /// 根据条件得到尝试授权用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> expression);

        /// <summary>
        /// 用户信息存在则更新，否则插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<UserEntity> AddOrUpdateAsync(UserEntity entity);


    }
}
