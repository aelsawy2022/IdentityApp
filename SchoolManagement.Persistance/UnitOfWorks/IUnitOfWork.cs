using SchoolManagement.Persistance.Repositories.ActivityRepo;
using SchoolManagement.Persistance.Repositories.AddressRepo;
using SchoolManagement.Persistance.Repositories.ClassRepo;
using SchoolManagement.Persistance.Repositories.ClassUserRepo;
using SchoolManagement.Persistance.Repositories.GradeRepo;
using SchoolManagement.Persistance.Repositories.ManagementRepo;
using SchoolManagement.Persistance.Repositories.RoleRepo;
using SchoolManagement.Persistance.Repositories.SchoolRepo;
using SchoolManagement.Persistance.Repositories.SeasonRepo;
using SchoolManagement.Persistance.Repositories.UserRepo;
using SchoolManagement.Persistance.Repositories.UserTypeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.UnitOfWorks
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
        IRoleRepository RoleRepository { get; }


        void Save();
        Task SaveAsync();
    }
}
