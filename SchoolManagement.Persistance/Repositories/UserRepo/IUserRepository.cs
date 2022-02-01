using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersWithRolesAsync(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false);
    }
}
