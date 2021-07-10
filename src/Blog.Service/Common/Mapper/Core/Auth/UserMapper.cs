using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Domains.Entities.Core;
using Blog.Service.Common.Common.Converter;
using Blog.Service.Core.Auth.Output.GitHub;

namespace Blog.Service.Common.Mapper.Core.Auth
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserOutput, UserEntity>()
                .ForMember(d =>d.Id, opt=>opt.Ignore())
                .ForMember(d => d.UserId, opt=>opt.MapFrom(src => src.Id))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(src => src.Avatar_url))
                .ForMember(d => d.HtmlUrl, opt => opt.MapFrom(src => src.Html_url))
                .ForMember(d => d.ReposUrl, opt => opt.MapFrom(src => src.Repos_url))
                .ForMember(d => d.PublicRepos, opt => opt.MapFrom(src => src.Public_repos));
        }
    }
}
