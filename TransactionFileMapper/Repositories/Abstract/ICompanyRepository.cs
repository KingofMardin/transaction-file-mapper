using MongoDB.Driver;
using TransactionFileMapper.Entities.Concrete;

namespace TransactionFileMapper.Repositories.Abstract
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public List<Company> GetCompaniesWithSubsidiaries();
        Company GetByFilter(FilterDefinition<Company> filter);
    }
}
