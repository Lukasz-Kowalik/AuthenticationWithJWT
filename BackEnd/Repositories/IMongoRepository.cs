using BackEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEnd.Generics
{
    public interface IMongoRepository<T> where T : IDocument
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        Task<T> GetByIdAsync(string id);

        void Insert(T obj);

        void Update(T obj);

        void DeleteOne(Expression<Func<T, bool>> filterExpression);
    }
}