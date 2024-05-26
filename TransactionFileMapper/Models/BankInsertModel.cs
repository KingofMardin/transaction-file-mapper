namespace TransactionFileMapper.Models
{
    public class BankInsertModel
    {
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string? RequestUrl { get; set; }
    }
}