namespace TransactionFileMapper.Models
{
    public class CompanyInserModel
    {
        public string CompanyName { get; set; }
        public List<SubsidiaryInsertModel> Subsidiaries { get; set; }
    }
}
