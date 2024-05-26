using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.Services.Concrete
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
           _bankRepository = bankRepository;
        }

        public void DeleteById(ObjectId Id)
        {
            _bankRepository.DeleteById(Id);
        }

        public async Task DeleteByIdAsync(ObjectId Id)
        {
            await _bankRepository.DeleteByIdAsync(Id);
        }

        public void DeleteMany(Expression<Func<Bank, bool>> filter)
        {
            _bankRepository.DeleteMany(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<Bank, bool>> filter)
        {
            await _bankRepository.DeleteManyAsync(filter);
        }

        public void DeleteOne(Expression<Func<Bank, bool>> filter)
        {
            _bankRepository.DeleteOne(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<Bank, bool>> filter)
        {
           await _bankRepository.DeleteOneAsync(filter);
        }

        public IEnumerable<Bank> FilterBy(Expression<Func<Bank, bool>> filter)
        {
            return _bankRepository.FilterBy(filter);
        }

        public async Task<IEnumerable<Bank>> FilterByAsync(Expression<Func<Bank, bool>> filter)
        {
            return await _bankRepository.FilterByAsync(filter);
        }

        public IEnumerable<Bank> GetAll()
        {
            return _bankRepository.GetAll();
        }

        public async Task<IEnumerable<Bank>> GetAllAsAsync()
        {
            return await _bankRepository.GetAllAsAsync();
        }

        public Bank GetByFilter(FilterDefinition<Bank> filter)
        {
            return _bankRepository.GetByFilter(filter);
        }

        public Bank GetById(ObjectId Id)
        {
            return _bankRepository.GetById(Id);
        }

        public async Task<Bank> GetByIdAsync(ObjectId Id)
        {
            return await _bankRepository.GetByIdAsync(Id);
        }

        public void InsertMany(IEnumerable<Bank> entities)
        {
           _bankRepository.InsertMany(entities);
        }

        public async Task InsertManyAsync(IEnumerable<Bank> entities)
        {
            await _bankRepository.InsertManyAsync(entities);
        }

        public void InsertOne(Bank entity)
        {
           _bankRepository.InsertOne(entity);
        }

        public async Task InsertOneAsync(Bank entity)
        {
            await _bankRepository.InsertOneAsync(entity);
        }

        public void ReplaceOne(Bank entity, ObjectId Id)
        {
           _bankRepository.ReplaceOne(entity, Id);
        }

        public async Task ReplaceOneAsync(Bank entity, ObjectId Id)
        {
            await _bankRepository.ReplaceOneAsync(entity, Id);
        }
    }
}
