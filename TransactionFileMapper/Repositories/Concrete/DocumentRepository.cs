using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TransactionFileMapper.DataAccess.Mongo;
using TransactionFileMapper.DataAccess.Settings;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;

namespace TransactionFileMapper.Repositories.Concrete
{
    public class DocumentRepository : GenericRepository<MT940>, IDocumentRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<MT940> _files;
        public DocumentRepository(IOptions<MongoConnection> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _files = _context.GetCollection<MT940>();
        }

        public List<MT940> GetDocumentsByFilter(FilterDefinition<MT940> filter)
        {
            return _files.Find(filter).ToList();
        }
    }
}
