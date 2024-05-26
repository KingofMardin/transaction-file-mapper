namespace TransactionFileMapper.Models
{
    public class SubsidiaryUpdateModel
    {
        public  string  SubsidiaryId { get; set; }
        public string? SubsidiaryName { get; set; }
        public List<BankUpdateModel> Banks { get; set; }
    }
}
