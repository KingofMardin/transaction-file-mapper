using TransactionFileMapper.Entities.Abstract;

namespace TransactionFileMapper.Entities.Concrete
{
    public class Bank : BaseEntity
    {
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string? RequestUrl { get; set; }
    }
}
