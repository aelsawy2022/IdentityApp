using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Persistance.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Persistance.Repositories.UserRepo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public virtual async Task<IEnumerable<User>> GetUsersWithRolesAsync(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
        {
            try
            {
                var result = await GetQueryable(filter, orderBy, includeProperties, take, skip, asNoTracking).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IQueryable<User> GetQueryable(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
        {
            try
            {
                includeProperties = includeProperties ?? string.Empty;
                IQueryable<User> query = table;

                if (filter != null)
                    query = query.Where(filter);

                query = query.Include(u => u.Governorate).Include(u => u.UserRoles).ThenInclude(r => r.Role);

                if (orderBy != null)
                    query = orderBy(query);

                if (skip.HasValue)
                    query = query.Skip(skip.Value);

                if (take.HasValue)
                    query = query.Take(take.Value);

                if (asNoTracking)
                    query = query.AsNoTracking();

                return query;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
