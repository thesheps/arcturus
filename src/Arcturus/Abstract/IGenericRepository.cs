using System;
using System.Collections.Generic;

namespace Arcturus.Abstract
{
    public interface IGenericRepository<T>
    {
        T Get(int key);
        IList<T> Get(Func<T, bool> query);
        IList<T> GetAll();
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
    }
}