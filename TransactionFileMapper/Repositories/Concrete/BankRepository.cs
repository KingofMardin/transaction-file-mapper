using Microsoft.Extensions.Options;
using TransactionFileMapper.DataAccess.Settings;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Repositories.Abstract;

namespace TransactionFileMapper.Repositories.Concrete
{
    public class BankRepository : GenericRepository<Bank>, IBankRepository
    {
        public BankRepository(IOptions<MongoConnection> settings) : base(settings)
        {
        }
    }
}
