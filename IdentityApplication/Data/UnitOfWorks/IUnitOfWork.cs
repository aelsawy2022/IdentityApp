using IdentityApplication.Data.Repositories.ActivityRepo;
using IdentityApplication.Data.Repositories.AddressRepo;
using IdentityApplication.Data.Repositories.ClassRepo;
using IdentityApplication.Data.Repositories.ClassUserRepo;
using IdentityApplication.Data.Repositories.GradeRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
using IdentityApplication.Data.Repositories.SeasonRepo;
using IdentityApplication.Data.Repositories.UserRepo;
using IdentityApplication.Data.Repositories.UserTypeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ISchoolRepository SchoolRepository { get; }
        IAddressRepository AddressRepository { get; }
        IManagementRepository ManagementRepository { get; }
        IGradeRepository GradeRepository { get; }
        IClassRepository ClassRepository { get; }
        IUserTypeRepository UserTypeRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IUserRepository UserRepository { get; }
        IClassUserRepository ClassUserRepository { get; }
        ISeasonRepository SeasonRepository { get; }


        void Save();
        Task SaveAsync();
    }
}
