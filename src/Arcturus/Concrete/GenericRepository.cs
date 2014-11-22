using Arcturus.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Arcturus.Concrete
{
    public class EntityFrameworkRepository<T, TContext> : IGenericRepository<T>
        where T : class
        where TContext : DbContext
    {
        public EntityFrameworkRepository(IDbContextFactory<TContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IList<T> GetAll()
        {
            using (var context = _dbContextFactory.Create())
            {
                return context.Set<T>().ToList();
            }
        }

        public T Get(int key)
        {
            using (var context = _dbContextFactory.Create())
            {
                return context.Set<T>().Find(key);
            }
        }

        public IList<T> Get(Func<T, bool> func)
        {
            using (var context = _dbContextFactory.Create())
            {
                return context.Set<T>().Where(func).ToList();
            }
        }

        public void Insert(T item)
        {
            using (var context = _dbContextFactory.Create())
            {
                context.Set<T>().Add(item);
                context.SaveChanges();
            }
        }

        public void Update(T item)
        {
            using (var context = _dbContextFactory.Create())
            {
                var entity = context.Entry<T>(item);

                if (entity.State == EntityState.Detached)
                {
                    context.Set<T>().Attach(entity.Entity);
                }

                entity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(T item)
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
                    context.Set<T>().Attach(item);
                    context.Set<T>().Remove(item);
                }

                context.SaveChanges();
            }
        }

        private readonly IDbContextFactory<TContext> _dbContextFactory;
    }
}