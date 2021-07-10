using AutoMapper;
using Blog.Core.Domains.Entities.Blog;
using Blog.Service.Blog.Article.Output;
using Blog.Service.Blog.Category.Output;
using Blog.Service.Blog.Tag.Output;

namespace Blog.Service.Common.Mapper.Blog
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<TagEntity, TagDto>();

            CreateMap<ArticleEntity, ArticleDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(src=>src.Category));
        }
    }
}
