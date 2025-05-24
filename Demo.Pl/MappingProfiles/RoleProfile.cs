using AutoMapper;
using Demo.Pl.ViweModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.Pl.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>().ReverseMap();
        }
    }
}
