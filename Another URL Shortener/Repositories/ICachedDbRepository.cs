using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Another_URL_Shortener.Repositories
{
    public interface ICachedDbRepository<T>
    {
        void Add(T instance);
        void Update(T instance);
        void Delete(T instance);
        IQueryable<T> Query(); // executes chain of query on server -- it breaks the idea of repository
        Task<T> Get(Guid id);
        Task<List<T>> GetAll(); // fetches data in memory and then applies filters
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        Task<int> SaveContext();
        void ModifyContextState(T t);
    }
}