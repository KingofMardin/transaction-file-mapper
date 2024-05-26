using System.Text.RegularExpressions;
using TransactionFileMapper.Business.AreaMappers.Abstract;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;

namespace TransactionFileMapper.Business.AreaMappers.Concrete
{
    public class ClosingBalanceAFMapper : Mapper
    {
        private static Lazy<ClosingBalanceAFMapper> _instance = new Lazy<ClosingBalanceAFMapper>(() => new ClosingBalanceAFMapper());
        public static ClosingBalanceAFMapper Instance = _instance.Value;
        public List<string> patterns = new List<string>() { "([A-Z]+)(\\d{6})([A-Z]+)(\\d{1,},\\d+)$", "([A-Z]+)(\\d{6})([A-Z]{2})(\\d{1,},\\d+)$" };

        public override bool Map(string value, ref MT940 file, AreaTypeEnum areaType)
        {
            bool isSuccess = false;
            foreach (var pattern in patterns)
            {
                var match = Regex.Matches(value, pattern);
                if (match.Count > 0 && match[0].Success)
                {
                    switch (pattern)
                    {
                        //2310181018DR1331,74NTRFNONREF//FT2329123456   OUTGOING PAYMENT
                        //(\d{6})-(\d{4})(D|C)(\\d{1})(\d+,)(N)-----------(\w+)-(\w+)(\w+)//(\w+)(\d+)\s+(.+)
                        //"(d{6})(d{6})
                        case ("([A-Z]+)(\\d{6})([A-Z]+)(\\d{1,},\\d+)$"):
                            var closingBalance = new ClosingBalance();
                            var x = match[0] != null && match[0].Groups[1].Value == TransactionProcessTypeEnum.C.ToString() ? closingBalance.CreditDebitCode = TransactionProcessTypeEnum.C : closingBalance.CreditDebitCode = TransactionProcessTypeEnum.D;

                            closingBalance.BookDate = match[0].Groups[2].Value;
                            closingBalance.CurrencyCode = match[0].Groups[3].Value;
                            closingBalance.Balance = Convert.ToDecimal(match[0].Groups[4].Value);//culture kontrolü gerekebilir kuruş . yada , ayırma için
                            file.ClosingBalance = closingBalance;
                            isSuccess = true;
                            break;
                        default:
                            isSuccess = false;
                            break;
                    }
                }
                isSuccess = false;
                //_logger.Error();

            }
            return isSuccess;

        }
    }
}
