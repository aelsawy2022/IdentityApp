using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Persistance.Data;

namespace SchoolManagement.Persistance.Repositories.SeasonRepo
{
    public class SeasonRepository : Repository<Season>, ISeasonRepository
    {
        public SeasonRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
