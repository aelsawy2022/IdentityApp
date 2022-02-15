using AutoMapper;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using System.Linq;

namespace SchoolManagement.Core.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UsersModel>()
                .ForMember(dto => dto.Roles, conf => conf.MapFrom(r => r.UserRoles.Select(ur => ur.Role).ToList()))
                .ReverseMap();

            CreateMap<Role, RolesModel>().ReverseMap();
            CreateMap<ClassUser, ClassUsersModel>().ReverseMap();
            CreateMap<School, SchoolModel>().ReverseMap();
            CreateMap<Activity, ActivityModel>().ReverseMap();

            CreateMap<ActivityClass, ActivityClassModel>().ReverseMap();
            CreateMap<Address, AddressModel>().ReverseMap();
            CreateMap<Class, ClassModel>().ReverseMap();
            CreateMap<Governorate, GovernorateModel>().ReverseMap();
            CreateMap<Grade, GradeModel>().ReverseMap();
            CreateMap<Management, ManagementModel>().ReverseMap();
            CreateMap<Season, SeasonModel>().ReverseMap();
            //CreateMap<UserRoleModel, UserRole>().ReverseMap();
            CreateMap<UserType, UserTypeModel>().ReverseMap();
            CreateMap<ActivityClass, ActivityClassModel>().ReverseMap();
            CreateMap<ActivitySlot, ActivitySlotModel>().ReverseMap();
            CreateMap<ActivityUserType, ActivityUserTypeModel>().ReverseMap();

            //.ForMember(dto=> dto.Chain, conf => conf.MapFrom(r=>r.Chain))
        }
    }
}
