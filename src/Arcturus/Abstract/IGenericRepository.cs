using System;
using System.Collections.Generic;

namespace Arcturus.Abstract
{
    public interface IGenericRepository<TEntity>
    {
        TEntity Get(int key);
        IEnumerable<TEntity> Get(Func<TEntity, bool> query);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity item);
        void Update(TEntity item);
        void Update(IList<TEntity> items);
        void Delete(TEntity item);
        void Delete(Func<TEntity, bool> query);
    }
}