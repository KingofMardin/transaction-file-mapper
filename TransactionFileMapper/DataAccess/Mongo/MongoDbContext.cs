using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TransactionFileMapper.DataAccess.Settings;

namespace TransactionFileMapper.DataAccess.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoConnection> settings)
        {
            var str = settings.Value.ConnectionString;
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.Trim());

        public IMongoDatabase GetDatabase() => _database;
    }
}
