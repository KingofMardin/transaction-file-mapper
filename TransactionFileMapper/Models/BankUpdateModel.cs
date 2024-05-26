namespace TransactionFileMapper.Models
{
    public class BankUpdateModel
    {
        public  string BankId { get; set; }
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string? RequestUrl { get; set; }
    }
}
