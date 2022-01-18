using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.SchoolRepo
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
