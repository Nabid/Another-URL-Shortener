using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Configuration
{
    public interface IRepository<T> where T : Entity
    {
        void Save(T instance);
        void Update(T instance);
        void Delete(T instance);
        IQueryable<T> Query(); // executes chain of query on server -- it breaks the idea of repository
        T Get(Guid id);
        IEnumerable<T> GetAll(); // fetches data in memory and then applies filters
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}