using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.ClassUserRepo
{
    public class ClassUserRepository : Repository<ClassUser>, IClassUserRepository
    {
        public ClassUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
