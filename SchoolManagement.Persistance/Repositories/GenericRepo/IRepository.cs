using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Repositories.GenericRepo
{
    public interface IRepository<Entity> where Entity: class
    {
        void Save();
        Task SaveAsync();

        #region Command Async Functions
        Task AddAsync(Entity entity);
        Task AddAsync(IEnumerable<Entity> entities);
        Task UpdateAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task DeleteAsync(object id);
        #endregion

        #region Command Standerd Functions
        void Add(Entity entity);
        void Add(IEnumerable<Entity> entities);
        void Update(Entity entity);
        void Delete(Entity entity);
        void Delete(object id);
        #endregion

        #region Query Async Functions
        Task<Entity> GetOneAsync(Expression<Func<Entity, bool>> filter = null, string includeProperties = null, bool NoTracking = false);
        Task<Entity> GetFirstAsync(Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, bool NoTracking = false);
        Task<int> GetCountAsync(Expression<Func<Entity, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<Entity, bool>> filter = null);
        Task<IEnumerable<Entity>> GetAllAsync(Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, int? take = null, int? skip = null, bool NoTracking = false);
        Task<Entity> GetByIDAsync(object id);
        Task<IEnumerable<Entity>> GetAsync(Expression<Func<Entity, bool>> filter, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false);
        Task<IEnumerable<Entity>> GetFilteredAsync(Expression<Func<Entity, bool>> filter, bool orderIsAsc, Expression<Func<Entity, object>> orderBy = null);
        #endregion

        #region Query Standerd Functions
        IQueryable<Entity> GetQueryable(Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, int? take = null, int? skip = null, bool NoTracking = false);
        Entity GetOne(Expression<Func<Entity, bool>> filter = null, string includeProperties = "", bool NoTracking = false);
        Entity GetFirst(Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = "", bool NoTracking = false);
        int GetCount(Expression<Func<Entity, bool>> filter = null);
        bool GetExists(Expression<Func<Entity, bool>> filter = null);
        IEnumerable<Entity> GetAll(Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = null, int? take = null, int? skip = null, bool NoTracking = false);
        Entity GetByID(object id);
        IEnumerable<Entity> Get(Expression<Func<Entity, bool>> filter, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, string includeProperties = "", int? take = null, int? skip = null, bool asNoTracking = false);
        //List<Entity> SQLQuery(string sql, params object[] parameters);
        //IEnumerable<Entity> GetFromSql(string sql, params SqlParameter[] parameters);
        //int GetCountFromSql(string sql, SqlParameter[] spParams, string recCountParamName);
        #endregion
    }
}
