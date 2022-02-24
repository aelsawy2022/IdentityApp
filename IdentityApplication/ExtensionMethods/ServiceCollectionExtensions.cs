using SchoolManagement.Persistance.Repositories.ActivityRepo;
using SchoolManagement.Persistance.Repositories.AddressRepo;
using SchoolManagement.Persistance.Repositories.ClassRepo;
using SchoolManagement.Persistance.Repositories.ClassUserRepo;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using SchoolManagement.Persistance.Repositories.GovernorateRepo;
using SchoolManagement.Persistance.Repositories.GradeRepo;
using SchoolManagement.Persistance.Repositories.ManagementRepo;
using SchoolManagement.Persistance.Repositories.RoleRepo;
using SchoolManagement.Persistance.Repositories.SchoolRepo;
using SchoolManagement.Persistance.Repositories.SeasonRepo;
using SchoolManagement.Persistance.Repositories.UserRepo;
using SchoolManagement.Persistance.Repositories.UserTypeRepo;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Persistance.UnitOfWorks;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Core.Services;
using SchoolManagement.Infrastructure.CustomFilters;

namespace IdentityApplication.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IGovernorateRepository), typeof(GovernorateRepository));
            services.AddScoped(typeof(IManagementRepository), typeof(ManagementRepository));
            services.AddScoped(typeof(ISchoolRepository), typeof(SchoolRepository));
            services.AddScoped(typeof(IAddressRepository), typeof(AddressRepository));
            services.AddScoped(typeof(IGradeRepository), typeof(GradeRepository));
            services.AddScoped(typeof(IClassRepository), typeof(ClassRepository));
            services.AddScoped(typeof(IUserTypeRepository), typeof(UserTypeRepository));
            services.AddScoped(typeof(IActivityRepository), typeof(ActivityRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IClassUserRepository), typeof(ClassUserRepository));
            services.AddScoped(typeof(ISeasonRepository), typeof(SeasonRepository));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));


            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            //General Lists Manager
            //services.AddScoped(typeof(IGeneralManager), typeof(GeneralManager));

        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IActivityService), typeof(ActivityService));
            services.AddScoped(typeof(IClassService), typeof(ClassService));
            services.AddScoped(typeof(IClassUserService), typeof(ClassUserService));
            services.AddScoped(typeof(IGovernorateService), typeof(GovernorateService));
            services.AddScoped(typeof(IGradesService), typeof(GradesService));
            services.AddScoped(typeof(IManagementService), typeof(ManagementService));
            services.AddScoped(typeof(ISchoolRoleService), typeof(SchoolRoleService));
            services.AddScoped(typeof(ISchoolService), typeof(SchoolService));
            services.AddScoped(typeof(ISeasonService), typeof(SeasonService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IUserTypeService), typeof(UserTypeService));
            services.AddScoped(typeof(IAuthenticateService), typeof(AuthenticateService));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IActivityInstanceService), typeof(ActivityInstanceService));
            services.AddScoped(typeof(IActivityLogService), typeof(ActivityLogService));
            services.AddScoped(typeof(IErrorLogService), typeof(ErrorLogService));

            //General Lists Manager
            //services.AddScoped(typeof(IGeneralManager), typeof(GeneralManager));

        }

        public static void RegisterCustomFilters(this IServiceCollection services)
        {
            services.AddScoped<CustomeExceptionFilter>();
            services.AddScoped<ActivityLoggingFilter>();
        }
    }
}
