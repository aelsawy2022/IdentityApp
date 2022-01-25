using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.SchoolRepo
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<IEnumerable<School>> GetSchoolWithActivitiesAndRolesAsync(Expression<Func<School, bool>> filter, Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false);
    }
}
