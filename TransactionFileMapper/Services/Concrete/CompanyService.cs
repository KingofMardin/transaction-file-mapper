using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.Services.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void DeleteById(ObjectId Id)
        {
            _companyRepository.DeleteById(Id);
        }

        public async Task DeleteByIdAsync(ObjectId Id)
        {
            await _companyRepository.DeleteByIdAsync(Id);
        }

        public void DeleteMany(Expression<Func<Company, bool>> filter)
        {
            _companyRepository.DeleteMany(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<Company, bool>> filter)
        {
            await _companyRepository.DeleteManyAsync(filter);
        }

        public void DeleteOne(Expression<Func<Company, bool>> filter)
        {
            _companyRepository.DeleteOne(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<Company, bool>> filter)
        {
            await _companyRepository.DeleteOneAsync(filter);
        }

        public IEnumerable<Company> FilterBy(Expression<Func<Company, bool>> filter)
        {
            return _companyRepository.FilterBy(filter);
        }

        public async Task<IEnumerable<Company>> FilterByAsync(Expression<Func<Company, bool>> filter)
        {
            return await _companyRepository.FilterByAsync(filter);
        }

        public IEnumerable<Company> GetAll()
        {
            return _companyRepository.GetAll();
        }

        public async Task<IEnumerable<Company>> GetAllAsAsync()
        {
            return await _companyRepository.GetAllAsAsync();
        }

        public Company GetByFilter(FilterDefinition<Company> filter)
        {
            return _companyRepository.GetByFilter(filter);
        }

        public Company GetById(ObjectId Id)
        {
            return _companyRepository.GetById(Id);
        }

        public async Task<Company> GetByIdAsync(ObjectId Id)
        {
            return await _companyRepository.GetByIdAsync(Id);
        }

        public List<Company> GetCompaniesWithSubsidiaries()
        {
            return _companyRepository.GetCompaniesWithSubsidiaries();
        }

        public void GetCompanyMt940()
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<Company> entities)
        {
            _companyRepository.InsertMany(entities);
        }

        public async Task InsertManyAsync(IEnumerable<Company> entities)
        {
            await _companyRepository.InsertManyAsync(entities);
        }

        public void InsertOne(Company entity)
        {
            _companyRepository.InsertOne(entity);
        }

        public async Task InsertOneAsync(Company entity)
        {
            await _companyRepository.InsertOneAsync(entity);
        }

        public void ReplaceOne(Company entity, ObjectId Id)
        {
            _companyRepository.ReplaceOne(entity, Id);
        }

        public async Task ReplaceOneAsync(Company entity, ObjectId Id)
        {
            await _companyRepository.ReplaceOneAsync(entity, Id);
        }
    }
}
