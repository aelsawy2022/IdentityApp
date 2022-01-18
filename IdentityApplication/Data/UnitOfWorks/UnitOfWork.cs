using IdentityApplication.Data.Repositories.AddressRepo;
using IdentityApplication.Data.Repositories.GradeRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private ISchoolRepository _schoolRepository;
        private IAddressRepository _addressRepository;
        private IManagementRepository _managementRepository;
        private IGradeRepository _gradeRepository;

        public UnitOfWork(
            ApplicationDbContext context,
            ISchoolRepository schoolRepository,
            IAddressRepository addressRepository,
            IManagementRepository managementRepository,
            IGradeRepository gradeRepository
            )
        {
            _context = context;
            _schoolRepository = schoolRepository;
            _addressRepository = addressRepository;
            _managementRepository = managementRepository;
            _gradeRepository = gradeRepository;
        }

        public ISchoolRepository SchoolRepository
        {
            get
            {
                return _schoolRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                return _addressRepository;
            }
        }

        public IManagementRepository ManagementRepository
        {
            get
            {
                return _managementRepository;
            }
        }

        public IGradeRepository GradeRepository
        {
            get
            {
                return _gradeRepository;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this._context == null)
            {
                return;
            }

            this._context.Dispose();
            this._context = null;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
