using System.Threading.Tasks;
using Blog.Core.Domains.Common;
using Blog.Service.Blog.Article.Input;
using Blog.Service.Blog.Article.Output;

namespace Blog.Service.Blog.Article
{
    public interface IArticleSvc
    {
        /// <summary>
        /// 获取文章分页数据
        /// </summary>
        /// <param name="pagingDto">分页参数</param>
        /// <returns></returns>
        Task<ServiceResult<PagedDto<ArticleDto>>> GetPagesAsync(ArticlePagingDto pagingDto);
    }
}
