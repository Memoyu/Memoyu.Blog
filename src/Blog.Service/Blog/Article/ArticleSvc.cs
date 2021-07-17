using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Core.Extensions;
using Blog.Core.Interface.IRepositories.Blog;
using Blog.Service.Base;
using Blog.Service.Blog.Article.Input;
using Blog.Service.Blog.Article.Output;
using Microsoft.Extensions.Logging;

namespace Blog.Service.Blog.Article
{
    public class ArticleSvc : ApplicationSvc, IArticleSvc
    {
        private readonly IArticleRepo _articleRepo;

        public ArticleSvc(IArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public async Task<ServiceResult<PagedDto<ArticleDto>>> GetPagesAsync(ArticlePagingDto pagingDto)
        {
            pagingDto.Sort = pagingDto.Sort.IsNullOrEmpty() ? "create_time DESC" : pagingDto.Sort.Replace("-", " ");
            var articles = await _articleRepo
                .Select
                .Include(a => a.Category)
                .Include(a => a.User)
                .IncludeMany(a => a.Tags)
                .Where(a => !a.IsDeleted)
                .WhereIf(pagingDto.IsTop.HasValue, a => a.IsTop == pagingDto.IsTop)
                .WhereIf(pagingDto.CategoryId > 0, a => a.CategoryId == pagingDto.CategoryId)
                .WhereIf(!string.IsNullOrWhiteSpace(pagingDto.Title), a => a.Title.Contains(pagingDto.Title))
                .WhereIf(!string.IsNullOrWhiteSpace(pagingDto.Author), a => a.Title.Contains(pagingDto.Author))
                .WhereIf(pagingDto.CreateTimeStart != null, a => a.CreateTime <= pagingDto.CreateTimeStart && a.CreateTime <= pagingDto.CreateTimeEnd)
                .OrderBy(pagingDto.Sort)
                .ToPageListAsync(pagingDto, out long total);

            var dtos = articles.Select(a => Mapper.Map<ArticleDto>(a)).ToList();
            return await Task.FromResult(ServiceResult<PagedDto<ArticleDto>>.Successed(new PagedDto<ArticleDto>(dtos, total)));

        }

        public async Task<ServiceResult<ArticleDto>> GetAsync(long id)
        {
            var article = await _articleRepo
                    .Select
                    .Include(a => a.Category)
                    .Include(a => a.User)
                    .IncludeMany(a => a.Tags)
                    .LeftJoin(a=>a.ArticleContent.ArticleId == a.Id)
                    .Where(a => a.Id == id)
                    .ToOneAsync();
            if (article == null) return await Task.FromResult(ServiceResult<ArticleDto>.Failed($"Id:{id} 的文章不存在"));
            return await Task.FromResult(ServiceResult<ArticleDto>.Successed(Mapper.Map<ArticleDto>(article)));
        }
    }
}
