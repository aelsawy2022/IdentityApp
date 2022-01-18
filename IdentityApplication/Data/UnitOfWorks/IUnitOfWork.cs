using IdentityApplication.Data.Repositories.AddressRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
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


        void Save();
        Task SaveAsync();
    }
}
