using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.SchoolRepo
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context) : base(context)
        {
        }

        public virtual async Task<IEnumerable<School>> GetSchoolWithActivitiesAndRolesAsync(Expression<Func<School, bool>> filter, Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
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


        public IQueryable<School> GetQueryable(Expression<Func<School, bool>> filter, Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
        {
            try
            {
                includeProperties = includeProperties ?? string.Empty;
                IQueryable<School> query = table;

                if (filter != null)
                    query = query.Where(filter);

                query = query.Include(s => s.Roles).Include(s => s.Activities).ThenInclude(a => a.Roles);

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

        //public virtual async Task<IQueryable<School>> GetSchoolWithActivitiesAndRolesAsync(Expression<Func<School, bool>> filter, Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
        //{
        //    try
        //    {
        //        includeProperties = includeProperties ?? string.Empty;
        //        IQueryable<School> query = table;

        //        if (filter != null)
        //            query = query.Where(filter);

        //        //foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        //{
        //        //    query = query.Include(includeProperty);
        //        //}

        //        query = query.Include(s => s.Roles).Include(s => s.Activities).ThenInclude(a => a.Roles);

        //        if (orderBy != null)
        //            query = orderBy(query);

        //        if (skip.HasValue)
        //            query = query.Skip(skip.Value);

        //        if (take.HasValue)
        //            query = query.Take(take.Value);

        //        if (asNoTracking)
        //            query = query.AsNoTracking();

        //        return query;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

    }
}
