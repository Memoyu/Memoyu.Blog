using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Core
{
    public interface IAdminUserRepo : IAuditBaseRepo<AdminUserEntity>
    {
        /// <summary>
        /// 获取全部授权管理员信息
        /// </summary>
        /// <returns></returns>
        Task<List<AdminUserEntity>> GetsAsync();

        /// <summary>
        /// 更新管理员最后登录时间
        /// </summary>
        /// <param name="userId">管理员Id</param>
        /// <returns></returns>
        Task UpdateLastLoginTimeAsync(long userId);
    }
}
