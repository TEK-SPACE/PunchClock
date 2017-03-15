using System;
using System.Linq;
using System.Linq.Expressions;

namespace PunchClock.Interface
{
    public interface IEntityRepository<TEntity> : IDisposable
    {
        IQueryable<TEntity> All { get; }
        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity Find(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void InsertOrUpdate(TEntity entity);
        void Delete(int id);
        //void Save();
    }
}
