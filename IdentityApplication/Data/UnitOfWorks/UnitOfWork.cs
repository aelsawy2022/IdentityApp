using IdentityApplication.Data.Repositories.ActivityRepo;
using IdentityApplication.Data.Repositories.AddressRepo;
using IdentityApplication.Data.Repositories.ClassRepo;
using IdentityApplication.Data.Repositories.GradeRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
using IdentityApplication.Data.Repositories.UserRepo;
using IdentityApplication.Data.Repositories.UserTypeRepo;
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
        private IClassRepository _classRepository;
        private IUserTypeRepository _userTypeRepository;
        private IActivityRepository _activityRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(
            ApplicationDbContext context,
            ISchoolRepository schoolRepository,
            IAddressRepository addressRepository,
            IManagementRepository managementRepository,
            IGradeRepository gradeRepository,
            IClassRepository classRepository,
            IUserTypeRepository userTypeRepository,
            IActivityRepository activityRepository,
            IUserRepository userRepository
            )
        {
            _context = context;
            _schoolRepository = schoolRepository;
            _addressRepository = addressRepository;
            _managementRepository = managementRepository;
            _gradeRepository = gradeRepository;
            _classRepository = classRepository;
            _userTypeRepository = userTypeRepository;
            _activityRepository = activityRepository;
            _userRepository = userRepository;
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

        public IClassRepository ClassRepository
        {
            get
            {
                return _classRepository;
            }
        }

        public IUserTypeRepository UserTypeRepository
        {
            get
            {
                return _userTypeRepository;
            }
        }

        public IActivityRepository ActivityRepository
        {
            get
            {
                return _activityRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository;
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
