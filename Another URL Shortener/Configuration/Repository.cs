using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Another_URL_Shortener.Models;
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
        }

        IEnumerable<T> IRepository<T>.Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        T IRepository<T>.Get(Guid id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        IQueryable<T> IRepository<T>.Query()
        {
            return _entities;
        }

        void IRepository<T>.Save(T instance)
        {
            _entities.Add(instance);
        }

        void IRepository<T>.Update(T instance)
        {
            _entities.Update(instance);
        }
    }
}