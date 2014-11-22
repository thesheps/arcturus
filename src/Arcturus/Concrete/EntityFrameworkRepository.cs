using Arcturus.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Arcturus.Concrete
{
    public class EntityFrameworkRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        public EntityFrameworkRepository(IDbContextFactory<DbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public TEntity Get(int key)
        {
            using (var context = _dbContextFactory.Create())
            {
                return context.Set<TEntity>().Find(key);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var context = _dbContextFactory.Create())
            {
                return context.Set<TEntity>();
            }
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> func)
        {
            using (var context = _dbContextFactory.Create())
            {
                return context.Set<TEntity>().Where(func);
            }
        }

        public void Insert(TEntity item)
        {
            using (var context = _dbContextFactory.Create())
            {
                context.Set<TEntity>().Add(item);
                context.SaveChanges();
            }
        }

        public void Update(TEntity item)
        {
            using (var context = _dbContextFactory.Create())
            {
                var entity = context.Entry<TEntity>(item);

                if (entity.State == EntityState.Detached)
                {
                    context.Set<TEntity>().Attach(entity.Entity);
                }

                entity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity item)
        {
            using (var context = _dbContextFactory.Create())
            {
                var dbEntityEntry = context.Entry(item);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    context.Set<TEntity>().Attach(item);
                    context.Set<TEntity>().Remove(item);
                }

                context.SaveChanges();
            }
        }

        public void Delete(Func<TEntity, bool> query)
        {
            using (var context = _dbContextFactory.Create())
            {
                var dbEntityEntry = context.Set<TEntity>().Where(query).First();
                context.Set<TEntity>().Remove(dbEntityEntry);
                context.SaveChanges();
            }
        }

        private readonly IDbContextFactory<DbContext> _dbContextFactory;
    }
}