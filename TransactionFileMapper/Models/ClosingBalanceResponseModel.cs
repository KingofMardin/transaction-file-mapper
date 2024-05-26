using TransactionFileMapper.Entities.Concrete;

namespace TransactionFileMapper.Models
{
    public class ClosingBalanceResponseModel
    {
        public string CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
    public class OpeningBalanceResponseModel
    {
        public string CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
    public class ClosingAvailableBalanceResponseModel
    {
        public string CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
    public class ForwardAvailableBalanceResponseModel
    {
        public string CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }

}
