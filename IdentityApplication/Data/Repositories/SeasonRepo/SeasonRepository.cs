using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.SeasonRepo
{
    public class SeasonRepository : Repository<Season>, ISeasonRepository
    {
        public SeasonRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
