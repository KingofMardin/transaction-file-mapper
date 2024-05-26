namespace TransactionFileMapper.Models
{
    public class BankResponseModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string? RequestUrl { get; set; }
    }
}
