using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalEdge.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : class
    {
        #region Properties
        internal DigitalEdgeContext _DigitalEdgeContext;
        internal DbSet<TEntity> dbSet;
        #endregion

        #region constructor
        public BaseRepository(DigitalEdgeContext DigitalEdgeContext)
        {
            this._DigitalEdgeContext = DigitalEdgeContext;
            this.dbSet = _DigitalEdgeContext.Set<TEntity>();
        }
        #endregion

        #region Getter methods
        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }


        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetQuery(filter, includeProperties);

            if (orderBy != null)
                return orderBy(query).ToList();

            return query.ToList();
        }
        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Any(filter);
        }


        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, string>> thenBy, int pageNo, int pageSize, string order = "asc", params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            if (order == "asc")
                (query.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
            else
                (query.Where(whereCondition).OrderByDescending(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize, string order = "asc", params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            Type[] types = typeof(TEntity).GetInterfaces();
            if (whereCondition != null)
            {
                if (order == "asc")
                    query = (query.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
                else
                    query = (query.Where(whereCondition).OrderByDescending(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsEnumerable();
        }
        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, int>> orderBy, int pageNo, int pageSize, string order = "asc", params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            Type[] types = typeof(TEntity).GetInterfaces();


            if (whereCondition != null)
            {
                if (order == "asc")
                    query = (query.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
                else
                    query = (query.Where(whereCondition).OrderByDescending(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsEnumerable();
        }
        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, long>> orderBy, int pageNo, int pageSize, string order = "asc", params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            Type[] types = typeof(TEntity).GetInterfaces();


            if (whereCondition != null)
            {
                if (order == "asc")
                    query = (query.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
                else
                    query = (query.Where(whereCondition).OrderByDescending(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsEnumerable();
        }
        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, DateTime>> orderBy, int pageNo, int pageSize, string order = "asc", params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            Type[] types = typeof(TEntity).GetInterfaces();

            //here we re filter deleted item
            //if (types.Contains(typeof(IFlagRemove)))
            //{
            //    query = (query as IQueryable<IFlagRemove>).Where(e => !e.IsDeleted).Cast<TEntity>();
            //}

            if (whereCondition != null)
            {
                if (order == "asc")
                    query = (query.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
                else
                    query = (query.Where(whereCondition).OrderByDescending(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize));
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsEnumerable();
        }
        public virtual List<TEntity> Get(int limit, int skip, Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetQuery(filter, includeProperties);

            if (orderBy != null)
                return orderBy(query).Skip(skip).Take(limit).ToList();

            return query.Skip(skip).Take(limit).ToList();
        }

        public virtual List<TResult> GetAs<TResult>(Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties) where TResult : class
        {
            var query = GetQuery(filter, includeProperties);
            return query.Select(select).ToList();
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {

            var query = GetQuery(filter, includeProperties);

            if (orderBy != null)
                return orderBy(query).ToListAsync();

            return query.ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(int limit, int skip, Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties
          )
        {

            var query = GetQuery(filter, includeProperties);

            if (orderBy != null)
                return orderBy(query).Skip(skip).Take(limit).ToListAsync();

            return query.Skip(skip).Take(limit).ToListAsync();
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = GetQuery(filter);
            var count = query.Count();
            return count;
        }
        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            Type[] types = typeof(TEntity).GetInterfaces();

            //here we re filter deleted item
            //if (types.Contains(typeof(IFlagRemove)))
            //{
            //    query = (query as IQueryable<IFlagRemove>).Where(e => !e.IsDeleted).Cast<TEntity>();
            //}

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.FirstOrDefault(filter);
        }
        #endregion


        #region CRUD operations
        public virtual void Insert(TEntity entity)
        {
            try
            {

                dbSet.Add(entity);

                // _blazeContext.SaveChangesAsync();
                _DigitalEdgeContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public virtual string InsertData(TEntity entity)
        {
            try
            {

                dbSet.Add(entity);

                // _blazeContext.SaveChangesAsync();
                _DigitalEdgeContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual void InsertAll(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Add(entity);
            }
            _DigitalEdgeContext.SaveChanges();
        }
        public virtual void DeleteAll(List<TEntity> entities)
        {
            foreach (var entityToDelete in entities)
            {
                if (_DigitalEdgeContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                //if (entityToDelete is IFlagRemove)
                //{
                //    ((IFlagRemove)entityToDelete).IsDeleted = true;
                //    Update(entityToDelete);
                //}
                //else
                //{
                dbSet.Remove(entityToDelete);
                _DigitalEdgeContext.SaveChanges();
                //}
            }
        }
        public virtual void UpdateAll(List<TEntity> entities)
        {
            foreach (var entityToUpdate in entities)
            {
                if (_DigitalEdgeContext.Entry(entityToUpdate).State == EntityState.Detached)
                    dbSet.Attach(entityToUpdate);
                _DigitalEdgeContext.Entry(entityToUpdate).State = EntityState.Modified;
            }
            _DigitalEdgeContext.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_DigitalEdgeContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            _DigitalEdgeContext.SaveChanges();
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            if (_DigitalEdgeContext.Entry(entityToUpdate).State == EntityState.Detached)
                dbSet.Attach(entityToUpdate);

            _DigitalEdgeContext.Entry(entityToUpdate).State = EntityState.Modified;
            _DigitalEdgeContext.SaveChanges();
        }
        #endregion


        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._DigitalEdgeContext != null)
            {
                this._DigitalEdgeContext.Dispose();
                this._DigitalEdgeContext = null;
            }
        }
        #endregion
    }
}
