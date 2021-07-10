using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Core
{
    public interface IBaseTypeRepo : IAuditBaseRepo<BaseTypeEntity>
    {
    }
}
