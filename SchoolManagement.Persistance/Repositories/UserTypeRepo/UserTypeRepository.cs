using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Persistance.Data;

namespace SchoolManagement.Persistance.Repositories.UserTypeRepo
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
