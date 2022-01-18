using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Repositories.GenericRepo
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        public ApplicationDbContext _context = null;
        public DbSet<Entity> table = null;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this.table = context.Set<Entity>();
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Entity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Added;
                table.Add(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(IEnumerable<Entity> entities)
        {
            try
            {
                foreach (var Obj in entities)
                {
                    Add(Obj);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task AddAsync(Entity entity)
        {
            try
            {
                Add(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task AddAsync(IEnumerable<Entity> entities)
        {
            try
            {
                foreach (var Obj in entities)
                {
                    await AddAsync(Obj);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(Entity entity)
        {
            try
            {
                _context.Set<Entity>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void Delete(object id)
        {
            Entity entity = table.Find(id);
            Delete(entity);
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            try
            {
                Delete(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            try
            {
                Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual IEnumerable<Entity> Get(Expression<Func<Entity, bool>> filter, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
        {
            try
            {
                return GetQueryable(filter, orderBy, includeProperties, take, skip, asNoTracking).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual IEnumerable<Entity> GetAll(Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, int? take = null, int? skip = null, bool NoTracking = false)
        {
            try
            {
                return GetQueryable(null, orderBy, includeProperties, take, skip, NoTracking).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync(Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, int? take = null, int? skip = null, bool NoTracking = false)
        {
            try
            {
                return await GetQueryable(null, orderBy, includeProperties, take, skip, NoTracking).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<Entity>> GetAsync(Expression<Func<Entity, bool>> filter, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false)
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

        public virtual Entity GetByID(object id)
        {
            try
            {
                return table.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<Entity> GetByIDAsync(object id)
        {
            try
            {
                return await table.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual int GetCount(Expression<Func<Entity, bool>> filter = null)
        {
            try
            {
                return GetQueryable(filter).Count();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<int> GetCountAsync(Expression<Func<Entity, bool>> filter = null)
        {
            try
            {
                return GetQueryable(filter).CountAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual int GetCountFromSql(string sql, SqlParameter[] spParams, string recCountParamName)
        {
            int RecCount = 0;
            try
            {

                this._context.Database.ExecuteSqlRaw(sql, spParams);
                var recCountParam = spParams.FirstOrDefault(p => p.ParameterName == recCountParamName);
                if (recCountParam != null)
                {
                    RecCount = int.Parse(recCountParam.Value.ToString());

                }

                return RecCount;
                //return this.table.FromSqlRaw(sql, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual bool GetExists(Expression<Func<Entity, bool>> filter = null)
        {
            try
            {
                return GetQueryable(filter).Any();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<Entity, bool>> filter = null)
        {
            try
            {
                return GetQueryable(filter).AnyAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<Entity>> GetFilteredAsync(Expression<Func<Entity, bool>> filter, bool orderIsAsc, Expression<Func<Entity, object>> orderBy = null)
        {
            IQueryable<Entity> query = table.Where(filter);
            try
            {
                if (orderBy != null)
                {
                    if (orderIsAsc)
                    {
                        query = query.OrderBy(orderBy);
                    }
                    else
                    {
                        query = query.OrderByDescending(orderBy);
                    }
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public virtual Entity GetFirst(Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = "", bool NoTracking = false)
        {
            try
            {
                return GetQueryable(filter, orderBy, includeProperties, null, null, NoTracking).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<Entity> GetFirstAsync(Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, bool NoTracking = false)
        {
            try
            {
                return await GetQueryable(filter, orderBy, includeProperties, null, null, NoTracking).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Entity> GetFromSql(string sql, params SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual Entity GetOne(Expression<Func<Entity, bool>> filter = null, string includeProperties = "", bool NoTracking = false)
        {
            try
            {
                return GetQueryable(filter, null, includeProperties, null, null, NoTracking).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<Entity> GetOneAsync(Expression<Func<Entity, bool>> filter = null, string includeProperties = null, bool NoTracking = false)
        {
            try
            {
                return await GetQueryable(filter, null, includeProperties, null, null, NoTracking).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Entity> GetQueryable(Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, int? take = null, int? skip = null, bool NoTracking = false)
        {
            try
            {
                includeProperties = includeProperties ?? string.Empty;
                IQueryable<Entity> query = table;

                if (filter != null)
                    query = query.Where(filter);

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                    query = orderBy(query);

                if (skip.HasValue)
                    query = query.Skip(skip.Value);

                if (take.HasValue)
                    query = query.Take(take.Value);

                if (NoTracking)
                    query = query.AsNoTracking();

                return query;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Entity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task UpdateAsync(Entity entity)
        {
            try
            {
                Update(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
