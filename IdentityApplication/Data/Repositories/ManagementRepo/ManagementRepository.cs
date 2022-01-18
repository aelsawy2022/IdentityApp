using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.ManagementRepo
{
    public class ManagementRepository : Repository<Management>, IManagementRepository
    {
        public ManagementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
