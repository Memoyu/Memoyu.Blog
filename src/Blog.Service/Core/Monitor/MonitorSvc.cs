using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Service.Base;
using Blog.Service.Core.Monitor.Output;

namespace Blog.Service.Core.Monitor
{
    public class MonitorSvc : ApplicationSvc, IMonitorSvc
    {
        private readonly IArticleRepo _articleRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ITagRepo _tagRepo;
        private readonly ICommentRepo _commentRepo;

        public MonitorSvc(IArticleRepo articleRepo, ICategoryRepo categoryRepo, ITagRepo tagRepo, ICommentRepo commentRepo)
        {
            _articleRepo = articleRepo;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
            _commentRepo = commentRepo;
        }

        public async Task<BlogInfoDto> GetBlogInfo()
        {
            var dto = new BlogInfoDto
            {
                ArticleTotal = await _articleRepo.Select.Where(a => !a.IsDeleted).CountAsync(),
                CategoryTotal = await _categoryRepo.Select.Where(c => !c.IsDeleted).CountAsync(),
                TagTotal = await _tagRepo.Select.Where(t => !t.IsDeleted).CountAsync(),
                CommentTotal = await _commentRepo.Select.Where(c => !c.IsDeleted).CountAsync()
            };
            return dto;
        }
    }
}
