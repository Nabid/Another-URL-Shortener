using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Another_URL_Shortener.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unity.Injection;

namespace Another_URL_Shortener.Configuration
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        void IRepository<T>.Delete(T instance)
        {
            _entities.Remove(instance);
            ((IRepository<T>)this).SaveContext();
        }

        IQueryable<T> IRepository<T>.Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        T IRepository<T>.Get(Guid id)
        {
            return _entities.Find(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        IQueryable<T> IRepository<T>.Query()
        {
            return _entities;
        }

        void IRepository<T>.Add(T instance)
        {
            _entities.Add(instance);
            ((IRepository<T>)this).SaveContext();
        }

        void IRepository<T>.Update(T instance)
        {
            _entities.Update(instance);
            ((IRepository<T>)this).SaveContext();
        }

        async Task<int> IRepository<T>.SaveContext()
        {
            return await _dbContext.SaveChangesAsync();
        }

        void IRepository<T>.ModifyContextState(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
        }
    }
}