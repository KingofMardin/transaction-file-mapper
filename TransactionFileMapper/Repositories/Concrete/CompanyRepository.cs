using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TransactionFileMapper.DataAccess.Mongo;
using TransactionFileMapper.DataAccess.Settings;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;

namespace TransactionFileMapper.Repositories.Concrete
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Company> _companies;
        public CompanyRepository(IOptions<MongoConnection> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _companies = _context.GetCollection<Company>();
        }

        public Company GetByFilter(FilterDefinition<Company> filter)
        {
            return _companies.Find(filter).FirstOrDefault();
        }

        //public Company GetByFilter(Expression<Func<Company, bool>> filter)
        //{
        //    var filter = Builders<Company>.Filter(;
        //    return _companies.Find(filter).FirstOrDefault();
        //}

        #region Example
        public List<Company> GetCompaniesWithSubsidiaries()
        {
            return _companies.AsQueryable().Where(x => x.Subsidiaries.Any()).ToList();
        }
        #endregion
    }
}
