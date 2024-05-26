using MongoDB.Driver;
using TransactionFileMapper.Entities.Concrete;

namespace TransactionFileMapper.Repositories.Abstract
{
    public interface IDocumentRepository : IGenericRepository<MT940>
    {
        public List<MT940> GetDocumentsByFilter(FilterDefinition<MT940> filter);

    }
}
