using BackEnd.Attributes;
using BackEnd.Helpers;
using BackEnd.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEnd.Generics
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IDatabaseSettings settings)
        {
            var _client = new MongoClient(settings.ConnectionString);

            var db = _client.GetDatabase(settings.DatabaseName);
            _collection = db.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToEnumerable();
        }

        public virtual T GetById(string id)
        {
            return _collection.Find(x => x.Id == new ObjectId(id)).FirstOrDefault();
        }

        public virtual T Get(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).FirstOrDefault();
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return Task.Run(() => _collection.Find(expression).FirstOrDefault());
        }

        public virtual Task<T> GetByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual void Insert(T obj)
        {
            _collection.InsertOne(obj);
        }

        public virtual void Update(T obj)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, obj.Id);
            _collection.FindOneAndReplace<T>(filter, obj);
        }
    }
}