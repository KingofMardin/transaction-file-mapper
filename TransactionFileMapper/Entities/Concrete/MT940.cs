using MongoDB.Bson;
using TransactionFileMapper.Entities.Abstract;

namespace TransactionFileMapper.Entities.Concrete
{
    public class MT940 : BaseEntity
    {
        /// <summary>
        /// This field specifies the reference assigned by the Sender to unambiguously identify the message. 20
        /// </summary>
        public string TransactionReferenceNumber { get; set; } = "20";

        public ObjectId SubsidiaryId { get; set; }
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// 25
        /// public AccountIdentification AccountIdentification { get; set; }//"25";
        /// </summary>
        public string RelatedReference { get; set; }
        public string? IdentifierCode { get; set; }
        /// <summary>
        /// 25 içerisinde
        /// </summary>
        public string? AccountOwnerCode { get; set; } = null;

        //public StatementAndSquenceNumber StatementAndSquenceNumber { get; set; } //28
        /// <summary>
        /// 28 parçası
        /// </summary>
        public string StamentNumber { get; set; }
        /// <summary>
        /// 28 parçası
        /// </summary>
        public string SequenceNumber { get; set; }

        public OpeningBalance OpeningBalance { get; set; } //60

        /// <summary>
        /// 61
        /// </summary>
        public List<Transaction> Transactions { get; set; } //61
                                                            //
                                                            //public AccountOwnerInformation AccountOwnerInformation { get; set; } //86
        public string AccountOwnerInformation { get; set; }

        public ClosingBalance ClosingBalance { get; set; } //62
        public ClosingAvailableBalance ClosingAvailableBalance { get; set; } //64
        public ForwardAvailableBalance ForwardAvailableBalance { get; set; } //65

    }
    public class AccountIdentification
    {
        /// <summary>
        /// 25
        /// </summary>
        public string IdentifierCode { get; set; }
        public string? AcocuntOwnerCode { get; set; } = null;
    }
    public class StatementAndSquenceNumber
    {
        /// <summary>
        /// 28
        /// </summary>
        public string SatementNumber { get; set; }
        public string SequenceNumber { get; set; }

    }
    public class OpeningBalance
    {
        /// <summary>
        /// 60
        /// </summary>
        public TransactionProcessTypeEnum CreditDebitCode { get; set; }
        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }


    }
    public class Transaction
    {
        /// <summary>
        /// 61
        /// </summary>
        public string TransactionDate { get; set; }
        public string BookedDate { get; set; }
        public TransactionProcessTypeEnum TransactionProcessType { get; set; } = TransactionProcessTypeEnum.C;
        public string CurrencyCodeLastLetter { get; set; }
        public decimal Amount { get; set; }
        public string? TransactionType { get; set; } = null;
        public string? IdentificationCodeEnum { get; set; } = null;
        /// <summary>
        /// 61 NONREF İçermiyorsa true yapılacak
        /// </summary>
        public bool HasAcocuntOwnerData { get; set; } = false;
        /// <summary>
        /// NONREF içeriyorsa gelen data ?? ne olduğu araştırılacak
        /// bankanın dekont/belge numarası
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;
        public string ProcessDescription { get; set; }

    }
    public class AccountOwnerInformation
    {
        /// <summary>
        /// 86
        /// </summary>
        public string Information { get; set; }
    }
    public class ClosingBalance
    {
        /// <summary>
        /// 62
        /// </summary>
        public TransactionProcessTypeEnum CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
    public class ClosingAvailableBalance
    {
        /// <summary>
        /// 64
        /// </summary>
        public TransactionProcessTypeEnum CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
    public class ForwardAvailableBalance
    {
        /// <summary>
        /// 65
        /// </summary>
        public TransactionProcessTypeEnum CreditDebitCode { get; set; }

        public string BookDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
    public enum ProcessDescriptionEnum
    {
        OUTGOING,
        ICOMING
    }
    public enum IdentificationCodeEnum
    {
        TRF
    }
    public enum CurrencyCodeEnum
    {
        EUR,
        TRY,
        USD,
        GBP,
        AUD,

    }
    public enum TransactionProcessTypeEnum
    {
        D,
        C,
        RC,
        RD
    }
}

