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
        T FindById(object id);
        IEnumerable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> searchTerm);
        IQueryable<T> FindByQuery(IQuery<T> query);
        IFutureValue<T> FutureFindById(object id);
        IFutureQueryOf<T> FutureFindAll();
        IFutureQueryOf<T> FutureFind(Expression<Func<T, bool>> searchTerm);
        IQueryable<T> Query();
    }

    public interface IFutureQueryOf<T> : IEnumerable<T>
    {
        int TotalCount { get; }
        ICollection<T> Results { get; }
        T SingleResult { get; }
    }

    public interface IFutureValue<T>
    {
        T Value { get; }
    }

    public class SqlParam
    {
        public SqlParam(string parameterName, object value, SqlParamType sqlParamType, string columnName)
        {
            ParameterName = parameterName;
            Value = value;
            SqlParamType = sqlParamType;
            ColumnName = columnName;
        }

        public string ColumnName { get; private set; }

        public string ParameterName { get; private set; }
        public object Value { get; private set; }

        public SqlParamType SqlParamType { get; private set; }

        public T GetParamValue<T>()
        {
            return (T)Value;
        }
    }

    public enum SqlParamType
    {
        String,
        StringWithLikeSearch,
        Date,
        UniqueIdentifier,
        Bit,
        Bigint,
        Int,
        TextWithoutQuote
    }
}