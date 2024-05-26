using TransactionFileMapper.Entities.Abstract;

namespace TransactionFileMapper.Entities.Concrete
{
    public class Subsidiary : BaseEntity
    {
        public Company? Company { get; set; }
        public string? SubsidiaryName { get; set; }
        public List<MT940> MT940 { get; set; }
        public List<Bank> Banks { get; set; }
    }
}
