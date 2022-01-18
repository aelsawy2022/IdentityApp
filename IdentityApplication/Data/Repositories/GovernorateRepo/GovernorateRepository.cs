using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.GovernorateRepo
{
    public class GovernorateRepository : Repository<Governorate>, IGovernorateRepository
    {
        public GovernorateRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
