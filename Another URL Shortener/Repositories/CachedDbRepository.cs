using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;
using Microsoft.EntityFrameworkCore;

namespace Another_URL_Shortener.Repositories
{
    public class CachedDbRepository<T> : ICachedDbRepository<T> where T : Entity
    {
        private readonly CacheDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public CachedDbRepository(CacheDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        void ICachedDbRepository<T>.Delete(T instance)
        {
            _entities.Remove(instance);
            ((ICachedDbRepository<T>)this).SaveContext();
        }

        IQueryable<T> ICachedDbRepository<T>.Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        async Task<T> ICachedDbRepository<T>.Get(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        IQueryable<T> ICachedDbRepository<T>.Query()
        {
            return _entities;
        }

        void ICachedDbRepository<T>.Add(T instance)
        {
            _entities.Add(instance);
            ((ICachedDbRepository<T>)this).SaveContext();
        }

        void ICachedDbRepository<T>.Update(T instance)
        {
            _entities.Update(instance);
            ((ICachedDbRepository<T>)this).SaveContext();
        }

        async Task<int> ICachedDbRepository<T>.SaveContext()
        {
            return await _dbContext.SaveChangesAsync();
        }

        void ICachedDbRepository<T>.ModifyContextState(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
        }
    }
}