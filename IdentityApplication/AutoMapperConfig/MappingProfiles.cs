using AutoMapper;
using IdentityApplication.Data.Entities;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;
namespace RMS_APIs.AutoMapperConfig
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UsersModel>()
                .ForMember(dto => dto.Roles, conf => conf.MapFrom(r => r.UserRoles))
                .ReverseMap();

            CreateMap<Role, RolesModel>().ReverseMap();
            CreateMap<ClassUser, ClassUsersModel>().ReverseMap();
            CreateMap<School, SchoolModel>().ReverseMap();
            CreateMap<Activity, ActivityModel>().ReverseMap();

            //.ForMember(dto=> dto.Chain, conf => conf.MapFrom(r=>r.Chain))
        }
    }
}
