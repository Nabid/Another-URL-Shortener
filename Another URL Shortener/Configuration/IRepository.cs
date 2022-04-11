using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Another_URL_Shortener.Services
{
    public interface IRepository<T> where T : class
    {
        void Save(T instance);
        void Update(T instance);
        void Delete(T instance);
        IQueryable<T> Query(); // Get all
    }
}