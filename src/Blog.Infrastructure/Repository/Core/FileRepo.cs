using Blog.Core.Common.Configs;
using Blog.Core.Domains.Entities.Core;
using Blog.Core.Interface.IRepositories.Core;
using Blog.Core.Security;
using Blog.Infrastructure.Repository.Base;
using FreeSql;
using Microsoft.Extensions.Options;

namespace Blog.Infrastructure.Repository.Core
{
    public class FileRepo : AuditBaseRepo<FileEntity>, IFileRepo
    {
        public FileRepo(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        public string GetFileUrl(string path)
        {
            if (string.IsNullOrEmpty(path)) return "";
            if (path.StartsWith("http") || path.StartsWith("https"))//如果是完整地址
            {
                return path;
            }

            if (path.StartsWith("core"))//如果是本地初始资源
            {
                return Appsettings.FileStorage.LocalFileHost + path;
            }


            FileEntity file = base.Where(r => r.Path == path).First();
            if (file == null) return path;
            switch (file.Type)
            {
                case 1:
                    return Appsettings.FileStorage.LocalFileHost + path;
                default:
                    return Appsettings.FileStorage.LocalFileHost + path;
            }
        }
    }
}
