using TransactionFileMapper.Entities.Concrete;

namespace TransactionFileMapper.Models
{
    public class GetMT940FileResponseModel
    {
        /// <summary>
        /// This field specifies the reference assigned by the Sender to unambiguously identify the message. 20
        /// </summary>
        public string DocumentId { get; set; }
        public string CreateDate { get; set; }
        public string TransactionReferenceNumber { get; set; } = "20";

        public string SubsidiaryId { get; set; }
        public string CompanyId { get; set; }

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

        public OpeningBalanceResponseModel OpeningBalance { get; set; } //60

        /// <summary>
        /// 61
        /// </summary>
        public List<TransactionResponseModel> Transactions { get; set; } //61
                                                            //
                                                            //public AccountOwnerInformation AccountOwnerInformation { get; set; } //86
        public string AccountOwnerInformation { get; set; }

        public ClosingBalanceResponseModel ClosingBalance { get; set; } //62
        public ClosingAvailableBalanceResponseModel ClosingAvailableBalance { get; set; } //64
        public ForwardAvailableBalanceResponseModel ForwardAvailableBalance { get; set; } //65
    }
    public class TransactionResponseModel
    {
        /// <summary>
        /// 61
        /// </summary>
        public string TransactionDate { get; set; }
        public string BookedDate { get; set; }
        public string TransactionProcessType { get; set; } 
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
}
