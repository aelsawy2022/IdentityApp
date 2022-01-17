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
            CreateMap<User, UsersModel>().ReverseMap();
            CreateMap<Role, RolesModel>().ReverseMap();

            //.ForMember(dto=> dto.Chain, conf => conf.MapFrom(r=>r.Chain))
        }
    }
}
