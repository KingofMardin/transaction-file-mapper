using TransactionFileMapper.Entities.Abstract;

namespace TransactionFileMapper.Entities.Concrete
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public List<Subsidiary> Subsidiaries { get; set; }
    }
}
