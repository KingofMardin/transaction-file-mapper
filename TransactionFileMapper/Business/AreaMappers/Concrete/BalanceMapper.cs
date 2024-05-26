using System.Text.RegularExpressions;
using TransactionFileMapper.Business.AreaMappers.Abstract;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;

namespace TransactionFileMapper.Business.AreaMappers.Concrete
{
    public class BalanceMapper : Mapper
    {
        private static readonly Lazy<BalanceMapper> _instance = new Lazy<BalanceMapper>(() => new BalanceMapper());
        public static BalanceMapper Instance = _instance.Value;

        string pattern = "([A-Z]+)(\\d{6})([A-Z]+)(\\d{1,},\\d+)$";
        public override bool Map(string value, ref MT940 file, AreaTypeEnum areaType)
        {
            bool isSuccess = false;
            var match = Regex.Matches(value, pattern);
            switch (areaType)
            {
                case AreaTypeEnum.OpeningBalance:
                    if (match.Count > 0 && match[0].Success)
                    {
                        var openingBalance = new OpeningBalance();
                        var x = match[0] != null && match[0].Groups[1].Value == TransactionProcessTypeEnum.C.ToString() ? openingBalance.CreditDebitCode = TransactionProcessTypeEnum.C : openingBalance.CreditDebitCode = TransactionProcessTypeEnum.D;

                        openingBalance.BookDate = match[0].Groups[2].Value;
                        openingBalance.CurrencyCode = match[0].Groups[3].Value;
                        openingBalance.Balance = Convert.ToDecimal(match[0].Groups[4].Value);//culture kontrolü gerekebilir kuruş . yada , ayırma için             
                        file.OpeningBalance = openingBalance;
                        isSuccess = true;

                        return isSuccess;
                    }
                    break;
                case AreaTypeEnum.ClosingBalanceAF:
                    var closingBalanceAF = new ClosingAvailableBalance();
                    if (match.Count > 0 && match[0].Success)
                    {
                        var x = match[0] != null && match[0].Groups[1].Value == TransactionProcessTypeEnum.C.ToString() ? closingBalanceAF.CreditDebitCode = TransactionProcessTypeEnum.C : closingBalanceAF.CreditDebitCode = TransactionProcessTypeEnum.D;

                        closingBalanceAF.BookDate = match[0].Groups[2].Value;
                        closingBalanceAF.CurrencyCode = match[0].Groups[3].Value;
                        closingBalanceAF.Balance = Convert.ToDecimal(match[0].Groups[4].Value);//culture kontrolü gerekebilir kuruş . yada , ayırma için
                        file.ClosingAvailableBalance = closingBalanceAF;
                        isSuccess = true;

                        return isSuccess;
                    }
                    break;
                case AreaTypeEnum.ClosingBalanceBF:
                    if (match.Count > 0 && match[0].Success)
                    {
                        var closingBalance = new ClosingBalance();
                        var x = match[0] != null && match[0].Groups[1].Value == TransactionProcessTypeEnum.C.ToString() ? closingBalance.CreditDebitCode = TransactionProcessTypeEnum.C : file.ClosingAvailableBalance.CreditDebitCode = TransactionProcessTypeEnum.D;

                        closingBalance.BookDate = match[0].Groups[2].Value;
                        closingBalance.CurrencyCode = match[0].Groups[3].Value;
                        closingBalance.Balance = Convert.ToDecimal(match[0].Groups[4].Value);//culture kontrolü gerekebilir kuruş . yada , ayırma için
                        file.ClosingBalance = closingBalance;
                        isSuccess = true;

                        return isSuccess;
                    }
                    break;
                case AreaTypeEnum.ForwardAvaliableBalance:
                    if (match.Count > 0 && match[0].Success)
                    {
                        var fwAvaliableBalance = new ForwardAvailableBalance();
                        var x = match[0] != null && match[0].Groups[1].Value == TransactionProcessTypeEnum.C.ToString() ? fwAvaliableBalance.CreditDebitCode = TransactionProcessTypeEnum.C : fwAvaliableBalance.CreditDebitCode = TransactionProcessTypeEnum.D;
                        fwAvaliableBalance.BookDate = match[0].Groups[2].Value;
                        fwAvaliableBalance.CurrencyCode = match[0].Groups[3].Value;
                        fwAvaliableBalance.Balance = Convert.ToDecimal(match[0].Groups[4].Value);//culture kontrolü gerekebilir kuruş . yada , ayırma için
                        file.ForwardAvailableBalance = fwAvaliableBalance;
                        isSuccess = true;

                        return isSuccess;
                    }
                    break;
                default:
                    return isSuccess;

            }


            return isSuccess;

            #region Example
            //foreach (var pattern in patterns)
            //{
            //    Match match = Regex.Match(value, pattern);
            //    if (match.Success)
            //    {
            //        switch (pattern)
            //        {
            //            //2310181018DR1331,74NTRFNONREF//FT2329123456   OUTGOING PAYMENT
            //            //(\d{6})-(\d{4})(D|C)(\\d{1})(\d+,)(N)-----------(\w+)-(\w+)(\w+)//(\w+)(\d+)\s+(.+)
            //            //"(d{6})(d{6})
            //            case ("([A-Z]+)(\\d{6})([A-Z]+)(\\d{1,},\\d+)$"):

            //                var x = match.Groups[1].Value == CDCodeEnum.C.ToString() ? file.OpeningBalance.CreditDebitCode = CDCodeEnum.C : file.OpeningBalance.CreditDebitCode = CDCodeEnum.D;

            //                file.OpeningBalance.BookDate = DateTime.ParseExact(match.Groups[2].Value, "yyMMdd", CultureInfo.InvariantCulture);
            //                file.OpeningBalance.CurrencyCode = match.Groups[3].Value;
            //                file.OpeningBalance.Balance = Convert.ToDecimal(match.Groups[4].Value);//culture kontrolü gerekebilir kuruş . yada , ayırma için
            //                isSuccess = true;
            //                break;
            //            default:
            //                isSuccess = false; break;
            //        }
            //    }
            //    isSuccess = false;
            //_logger.Error(); 
            #endregion

        }
    }
}
