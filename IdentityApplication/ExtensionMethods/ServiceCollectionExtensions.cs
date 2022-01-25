using IdentityApplication.Data.Repositories.ActivityRepo;
using IdentityApplication.Data.Repositories.AddressRepo;
using IdentityApplication.Data.Repositories.ClassRepo;
using IdentityApplication.Data.Repositories.ClassUserRepo;
using IdentityApplication.Data.Repositories.GenericRepo;
using IdentityApplication.Data.Repositories.GovernorateRepo;
using IdentityApplication.Data.Repositories.GradeRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.RoleRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
using IdentityApplication.Data.Repositories.SeasonRepo;
using IdentityApplication.Data.Repositories.UserRepo;
using IdentityApplication.Data.Repositories.UserTypeRepo;
using IdentityApplication.Data.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApplication.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void Register(this IServiceCollection services)
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
    }
}
