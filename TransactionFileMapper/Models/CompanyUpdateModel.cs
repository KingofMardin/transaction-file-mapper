namespace TransactionFileMapper.Models
{
    public class CompanyUpdateModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public List<SubsidiaryUpdateModel> Subsidiaries { get; set; }
    }
}
