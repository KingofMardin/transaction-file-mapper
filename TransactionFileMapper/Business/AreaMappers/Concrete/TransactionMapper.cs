using System.Globalization;
using System.Text.RegularExpressions;
using TransactionFileMapper.Business.AreaMappers.Abstract;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;

namespace TransactionFileMapper.Business.AreaMappers.Concrete
{
    public class TransactionMapper : Mapper
    {
        private static Lazy<TransactionMapper> _instance = new Lazy<TransactionMapper>(() => new TransactionMapper());
        public static TransactionMapper instance => _instance.Value;
        private List<string> _patterns = new List<string>() {
            @"^(\d{6})(\d{4})(\w{1})(\w{1})(\d+,\d+)(\w{1})(\w+)(\w{6})\/\/(\w+)\s*(.+)",
            @"^(\d{6})(\d{4})(\w+)(\w{1})(\d+,\d+)(\w{1})\/\/(\w+)\s*(.+)",
            @"^(\d{6})(\d{4})(\w+)(\w{1})(\d+,\d+)(\w{1})(\w+)(\w{6})\/\/(\w+)\s*(.+)",
            @"^(\d{6})(\d{4})(\w{1})(\w{1})(\d+,\d+)(\w{1})\/\/(\w+)\s*(.+)"
        };
        public override bool Map(string value, ref MT940 file, AreaTypeEnum areaType)
        {
            if (file.Transactions == null || file.Transactions.Count == 0)
            {
                file.Transactions = new List<Transaction>();
            }
            Transaction tran = new Transaction();
            bool result = false;
            foreach (var pattern in _patterns)
            {
                var match = Regex.Matches(value, pattern);
                if (match.Count > 0 && match[0].Success)
                {
                    switch (pattern)
                    {
                        case @"^(\d{6})(\d{4})(\w{1})(\w{1})(\d+,\d+)(\w{1})(\w+)(\w{6})\/\/(\w+)\s*(.+)": //regex düzenle amount

                            tran.TransactionDate = match[0].Groups[1].Value;
                            tran.BookedDate = match[0].Groups[2].Value;
                            tran.TransactionProcessType = match[0].Groups[3].Value == TransactionProcessTypeEnum.D.ToString() ? TransactionProcessTypeEnum.D : TransactionProcessTypeEnum.C;
                            tran.CurrencyCodeLastLetter = match[0].Groups[4].Value;
                            tran.Amount = Convert.ToDecimal(match[0].Groups[5].Value, CultureInfo.InvariantCulture);
                            tran.TransactionType = match[0].Groups[6].Value ?? default;
                            tran.IdentificationCodeEnum = match[0].Groups[7].ToString();
                            tran.HasAcocuntOwnerData = false;
                            tran.DocumentNumber = match[0].Groups[9].Value;
                            tran.ProcessDescription = match[0].Groups[10].Value;
                            file.Transactions.Add(tran);
                            result = true;
                            break;

                        case @"^(\d{6})(\d{4})(\w+)(\w{1})(\d+,\d+)(\w{1})\/\/(\w+)\s*(.+)":

                            tran.TransactionDate = match[0].Groups[1].Value;
                            tran.BookedDate = match[0].Groups[2].Value;
                            tran.TransactionProcessType = match[0] != null && match[0].Groups[3].Value == TransactionProcessTypeEnum.D.ToString() ? TransactionProcessTypeEnum.D : TransactionProcessTypeEnum.C;
                            tran.CurrencyCodeLastLetter = match[0].Groups[4].Value;
                            tran.Amount = Convert.ToDecimal(match[0].Groups[5].Value, CultureInfo.InvariantCulture);
                            tran.TransactionType = match[0].Groups[6].Value ?? default;
                            tran.IdentificationCodeEnum = match[0].Groups[7].ToString();
                            tran.HasAcocuntOwnerData = true;
                            tran.DocumentNumber = match[0].Groups[9].Value;
                            tran.ProcessDescription = match[0].Groups[10].Value;
                            file.Transactions.Add(tran);
                            result = true;

                            break;

                        case @"^(\d{6})(\d{4})(\w+)(\w{1})(\d+,\d+)(\w{1})(\w+)(\w{6})\/\/(\w+)\s*(.+)":
                            tran.TransactionDate = match[0].Groups[1].Value;
                            tran.BookedDate = match[0].Groups[2].Value;
                            tran.TransactionProcessType = match[0].Groups[3].Value == TransactionProcessTypeEnum.RD.ToString() ? TransactionProcessTypeEnum.RD : TransactionProcessTypeEnum.RC;
                            tran.CurrencyCodeLastLetter = match[0].Groups[4].Value;
                            tran.Amount = Convert.ToDecimal(match[0].Groups[5].Value, CultureInfo.InvariantCulture);
                            tran.TransactionType = match[0].Groups[6].Value ?? default;
                            tran.IdentificationCodeEnum = match[0].Groups[7].ToString();
                            tran.HasAcocuntOwnerData = false;
                            tran.DocumentNumber = match[0].Groups[9].Value;
                            tran.ProcessDescription = match[0].Groups[10].Value;
                            file.Transactions.Add(tran);
                            result = true;
                            break;
                        case @"^(\d{6})(\d{4})(\w{1})(\w{1})(\d+,\d+)(\w{1})\/\/(\w+)\s*(.+)":

                            tran.TransactionDate = match[0].Groups[1].Value;
                            tran.BookedDate = match[0].Groups[2].Value;
                            tran.TransactionProcessType = match[0] != null && match[0].Groups[3].Value == TransactionProcessTypeEnum.RD.ToString() ? TransactionProcessTypeEnum.RD : TransactionProcessTypeEnum.RC;
                            tran.CurrencyCodeLastLetter = match[0].Groups[4].Value;
                            tran.Amount = Convert.ToDecimal(match[0].Groups[5].Value, CultureInfo.InvariantCulture);
                            tran.TransactionType = match[0].Groups[6].Value ?? default;
                            tran.IdentificationCodeEnum = match[0].Groups[7].ToString();
                            tran.HasAcocuntOwnerData = true;
                            tran.DocumentNumber = match[0].Groups[9].Value;
                            tran.ProcessDescription = match[0].Groups[10].Value;
                            file.Transactions.Add(tran);
                            result = true;

                            break;

                        default:
                            break;
                    }
                }
            }
            return result;
        }
    }
}
