using IdentityApplication.Data.Repositories.AddressRepo;
using IdentityApplication.Data.Repositories.GenericRepo;
using IdentityApplication.Data.Repositories.GovernorateRepo;
using IdentityApplication.Data.Repositories.GradeRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
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


            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            //General Lists Manager
            //services.AddScoped(typeof(IGeneralManager), typeof(GeneralManager));

        }
    }
}
