﻿using Blog.Core.Domains.Entities.Blog;
using Blog.Core.Interface.IRepositories.Base;

namespace Blog.Core.Interface.IRepositories.Blog
{
    /// <summary>
    /// 文章分类Repo
    /// </summary>
    public interface ICategoryRepo : IAuditBaseRepo<CategoryEntity>
    {
    }
}
