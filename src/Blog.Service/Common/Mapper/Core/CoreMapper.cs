using System;
using AutoMapper;
using Blog.Core.Domains.Common.Enums.Base;
using Blog.Core.Domains.Entities.Core;
using Blog.Service.Core.Auth.Output;
using Blog.Service.Core.Auth.Output.GitHub;

namespace Blog.Service.Common.Mapper.Core
{
    public class CoreMapper : Profile
    {
        public CoreMapper()
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(d => d.UserTypeName, opt => opt.MapFrom(src => Enum.GetName(typeof(UserTypeEnums), src.UserType)));

            CreateMap<GitHubUserDto, UserEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(src => src.Avatar_url))
                .ForMember(d => d.UserUrl, opt => opt.MapFrom(src => src.Html_url));
        }
    }
}
