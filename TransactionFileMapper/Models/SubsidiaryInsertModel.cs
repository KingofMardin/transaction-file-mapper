using TransactionFileMapper.Entities.Concrete;

namespace TransactionFileMapper.Models
{
    public class SubsidiaryInsertModel
    {        
        public string? SubsidiaryName { get; set; }        
        public List<BankInsertModel> Banks { get; set; }
    }
}