using TransactionFileMapper.Entities.Concrete;

namespace TransactionFileMapper.Models
{
    public class CompanyResponseModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CompanyName { get; set; }
        public List<SubsidiaryResponseModel> Subsidiaries { get; set; }
    }
}
