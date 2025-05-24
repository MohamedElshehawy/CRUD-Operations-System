using AutoMapper;
using Demo.DAL.Entities;
using Demo.Pl.ViweModels;

namespace Demo.Pl.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserViewModel>();
        }
    }
}
