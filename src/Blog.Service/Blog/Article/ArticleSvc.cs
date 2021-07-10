using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Extensions;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Service.Base;
using Blog.Service.Blog.Article.Input;
using Blog.Service.Blog.Article.Output;

namespace Blog.Service.Blog.Article
{
    public class ArticleSvc : ApplicationSvc, IArticleSvc
    {
        private readonly IArticleRepo _articleRepo;

        public ArticleSvc(IArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public async Task<PagedDto<ArticleDto>> GetPagesAsync(ArticlePagingDto pagingDto)
        {
            pagingDto.Sort = pagingDto.Sort.IsNullOrEmpty() ? "create_time DESC" : pagingDto.Sort.Replace("-", " ");
            var articles = await _articleRepo
                .Select
                .Include(a => a.Category)
                .IncludeMany(a => a.Tags)
                .Where(a => !a.IsDeleted)
                .Where(a => a.IsTop == pagingDto.IsTop)
                .WhereIf(pagingDto.CategoryId > 0, a => a.CategoryId == pagingDto.CategoryId)
                .WhereIf(!string.IsNullOrWhiteSpace(pagingDto.Title), a => a.Title.Contains(pagingDto.Title))
                .WhereIf(!string.IsNullOrWhiteSpace(pagingDto.Author), a => a.Title.Contains(pagingDto.Author))
                .WhereIf(pagingDto.CreateTimeStart != null, a => a.CreateTime <= pagingDto.CreateTimeStart && a.CreateTime <= pagingDto.CreateTimeEnd)
                .OrderBy(pagingDto.Sort)
                .ToPageListAsync(pagingDto, out long total);

            var dto = articles.Select(a => Mapper.Map<ArticleDto>(a)).ToList();
            return await Task.FromResult(new PagedDto<ArticleDto>(dto, total));

        }
    }
}
