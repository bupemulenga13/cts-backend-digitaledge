using DigitalEdge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DigitalEdge.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Dispose();
        IQueryable<TEntity> GetAll();

        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);

        List<TEntity> Get(int limit, int skip, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);

        List<TResult> GetAs<TResult>(Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties) where TResult : class;
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAsync(int limit, int skip, Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        //TEntity GetById(object id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);

        void Insert(TEntity entity);
        /*int InsertData(TEntity entity)*/
        string InsertData(TEntity entity);
        void InsertAll(List<TEntity> entities);

        void DeleteAll(List<TEntity> entities);

        void UpdateAll(List<TEntity> entities);

        //void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
        bool Any(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, string>> thenBy, int pageNo, int pageSize, string order, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize, string order, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, int>> orderBy, int pageNo, int pageSize, string order, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, long>> orderBy, int pageNo, int pageSize, string order, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, DateTime>> orderBy, int pageNo, int pageSize, string order, params Expression<Func<TEntity, object>>[] includeProperties);


    }
}
