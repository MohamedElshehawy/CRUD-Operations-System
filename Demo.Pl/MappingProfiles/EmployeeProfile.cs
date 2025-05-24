using AutoMapper;
using Demo.DAL.Entities;
using Demo.Pl.ViweModels;

namespace Demo.Pl.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<EmployeeVM, Employee>().ReverseMap()/*.ForMember(d=>d.Name,opt=>opt.MapFrom(s=> s.EmployeeName))*/;
        }
    }
}
