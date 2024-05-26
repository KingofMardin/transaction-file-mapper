using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace TransactionFileMapper.Services.Abstract
{
    public interface IGenericService<T> where T : class
    {
        T GetByFilter(FilterDefinition<T> filter);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsAsync();
        IEnumerable<T> FilterBy(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filter);
        T GetById(ObjectId Id);
        Task<T> GetByIdAsync(ObjectId Id);
        void InsertOne(T entity);
        Task InsertOneAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entities);
        void InsertMany(IEnumerable<T> entities);
        void ReplaceOne(T entity, ObjectId Id);
        Task ReplaceOneAsync(T entity, ObjectId Id);
        void DeleteOne(Expression<Func<T, bool>> filter);
        void DeleteMany(Expression<Func<T, bool>> filter);
        Task DeleteManyAsync(Expression<Func<T, bool>> filter);
        Task DeleteOneAsync(Expression<Func<T, bool>> filter);
        void DeleteById(ObjectId Id);
        Task DeleteByIdAsync(ObjectId Id);

    }
}
