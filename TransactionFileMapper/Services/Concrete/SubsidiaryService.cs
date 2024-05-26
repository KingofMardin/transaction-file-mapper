using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.Services.Concrete
{
    public class SubsidiaryService : ISubsidiaryService
    {
        private readonly ISubsidiaryRepository _subsidiaryRepository;

        public SubsidiaryService(ISubsidiaryRepository subsidiaryRepository)
        {
            _subsidiaryRepository = subsidiaryRepository;
        }

        public void DeleteById(ObjectId Id)
        {
            _subsidiaryRepository.DeleteById(Id);
        }

        public async Task DeleteByIdAsync(ObjectId Id)
        {
            await _subsidiaryRepository.DeleteByIdAsync(Id);
        }

        public void DeleteMany(Expression<Func<Subsidiary, bool>> filter)
        {
            _subsidiaryRepository.DeleteMany(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<Subsidiary, bool>> filter)
        {
            await _subsidiaryRepository.DeleteManyAsync(filter);    
        }

        public void DeleteOne(Expression<Func<Subsidiary, bool>> filter)
        {
            _subsidiaryRepository.DeleteOne(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<Subsidiary, bool>> filter)
        {
            await _subsidiaryRepository.DeleteOneAsync(filter); 
        }

        public IEnumerable<Subsidiary> FilterBy(Expression<Func<Subsidiary, bool>> filter)
        {
            return _subsidiaryRepository.FilterBy(filter);
        }

        public async Task<IEnumerable<Subsidiary>> FilterByAsync(Expression<Func<Subsidiary, bool>> filter)
        {
           return await _subsidiaryRepository.FilterByAsync(filter);
        }

        public IEnumerable<Subsidiary> GetAll()
        {
            return _subsidiaryRepository.GetAll();
        }

        public async Task<IEnumerable<Subsidiary>> GetAllAsAsync()
        {
            return await _subsidiaryRepository.GetAllAsAsync();
        }

        public Subsidiary GetByFilter(FilterDefinition<Subsidiary> filter)
        {
            return _subsidiaryRepository.GetByFilter(filter);
        }

        public Subsidiary GetById(ObjectId Id)
        {
            return _subsidiaryRepository.GetById(Id);
        }

        public async Task<Subsidiary> GetByIdAsync(ObjectId Id)
        {
            return await _subsidiaryRepository.GetByIdAsync(Id);
        }

        public void InsertMany(IEnumerable<Subsidiary> entities)
        {
            _subsidiaryRepository.InsertMany(entities);
        }

        public async Task InsertManyAsync(IEnumerable<Subsidiary> entities)
        {
            await _subsidiaryRepository.InsertManyAsync(entities);
        }

        public void InsertOne(Subsidiary entity)
        {
            _subsidiaryRepository.InsertOne(entity);
        }

        public async Task InsertOneAsync(Subsidiary entity)
        {
            await _subsidiaryRepository.InsertOneAsync(entity);
        }

        public void ReplaceOne(Subsidiary entity, ObjectId Id)
        {
            _subsidiaryRepository.ReplaceOne(entity, Id);
        }

        public async Task ReplaceOneAsync(Subsidiary entity, ObjectId Id)
        {
            await _subsidiaryRepository.ReplaceOneAsync(entity, Id);
        }
    }
}
