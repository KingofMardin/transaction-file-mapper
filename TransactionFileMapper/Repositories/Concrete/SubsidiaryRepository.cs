using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TransactionFileMapper.DataAccess.Mongo;
using TransactionFileMapper.DataAccess.Settings;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;

namespace TransactionFileMapper.Repositories.Concrete
{
    public class SubsidiaryRepository : GenericRepository<Subsidiary>, ISubsidiaryRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Subsidiary> _subsidiaries;

        public SubsidiaryRepository(IOptions<MongoConnection> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _subsidiaries = _context.GetCollection<Subsidiary>();
        }
    }
}
