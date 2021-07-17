using AutoMapper;
using Blog.Core.Domains.Entities.Blog;
using Blog.Service.Blog.Article.Output;
using Blog.Service.Blog.Category.Input;
using Blog.Service.Blog.Category.Output;
using Blog.Service.Blog.Tag.Input;
using Blog.Service.Blog.Tag.Output;

namespace Blog.Service.Common.Mapper.Blog
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<AddCategoryDto, CategoryEntity>();
            CreateMap<ModifyCategoryDto, CategoryEntity>();

            CreateMap<TagEntity, TagDto>();
            CreateMap<AddTagDto, TagEntity>();
            CreateMap<ModifyTagDto, TagEntity>();

            CreateMap<ArticleContentEntity, ArticleContentDto>();
            CreateMap<ArticleEntity, ArticleDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(src=>src.Category));
        }
    }
}
