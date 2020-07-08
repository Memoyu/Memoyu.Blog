/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   命名空间 ：Memoyu.Blog.Application
*   文件名称 ：MemoyuBlogAutoMapperProfile
*   =================================
*   创 建 者 ：Memoyu
*   电子邮箱 ：mmy6076@outlook.com
*   创建日期 ：2020-07-05 15:03:40
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using AutoMapper;
using Memoyu.Blog.Application.Contracts.Blog;
using Memoyu.Blog.Application.Contracts.Blog.Params;
using Memoyu.Blog.Domain.Blog;

namespace Memoyu.Blog.Application
{
    class MemoyuBlogAutoMapperProfile : Profile
    {
        public MemoyuBlogAutoMapperProfile()
        {
            CreateMap<FriendLink, FriendLinkDto>();
            CreateMap<FriendLink, QueryFriendLinkForAdminDto>();
            CreateMap<EditPostInput, Post>().ForMember(p => p.Id, opt => opt.Ignore());//EditPostInput映射到Post，忽略Id
            CreateMap<EditCategoryInput, Category>().ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<EditTagInput, Tag>().ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<EditFriendLinkInput, FriendLink>().ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<Post, PostForAdminDto>().ForMember(x => x.Tags, opt => opt.Ignore());

        }
    }
}
