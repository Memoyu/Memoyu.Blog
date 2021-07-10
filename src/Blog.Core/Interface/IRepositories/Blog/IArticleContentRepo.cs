using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Blog
{
    /// <summary>
    /// 文章详情Repo
    /// </summary>
    public interface IArticleContentRepo : IAuditBaseRepo<ArticleContentEntity>
    {
    }
}
