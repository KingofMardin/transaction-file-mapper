using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using TransactionFileMapper.DataAccess.Mongo;
using TransactionFileMapper.DataAccess.Settings;
using TransactionFileMapper.Repositories.Abstract;

namespace TransactionFileMapper.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IOptions<MongoConnection> settings)
        {

            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<T>();
        }

        public void DeleteById(ObjectId Id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", Id);
                _collection.FindOneAndDelete(filter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteByIdAsync(ObjectId Id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", Id);
                await _collection.FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void DeleteMany(Expression<Func<T, bool>> filter)
        {
            try
            {

                _collection.DeleteMany(filter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            try
            {

                await _collection.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void DeleteOne(Expression<Func<T, bool>> filter)
        {
            try
            {
                _collection.DeleteOne(filter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                await _collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> FilterBy(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await _collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _collection.AsQueryable().ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsAsync()
        {
            try
            {
                return await _collection.AsQueryable().ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public T GetByFilter(FilterDefinition<T> filter)
        {
            return _collection.Find(filter).FirstOrDefault();
        }

        public T GetById(ObjectId Id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", Id);
                return _collection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetByIdAsync(ObjectId Id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", Id);
                return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void InsertMany(IEnumerable<T> entities)
        {
            try
            {
                _collection.InsertMany(entities);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task InsertManyAsync(IEnumerable<T> entities)
        {
            try
            {
                await _collection.InsertManyAsync(entities);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void InsertOne(T entity)
        {
            try
            {
                _collection.InsertOne(entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task InsertOneAsync(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void ReplaceOne(T entity, ObjectId Id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", Id);

                _collection.ReplaceOne(filter, entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task ReplaceOneAsync(T entity, ObjectId Id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", Id);

                await _collection.ReplaceOneAsync(filter, entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
