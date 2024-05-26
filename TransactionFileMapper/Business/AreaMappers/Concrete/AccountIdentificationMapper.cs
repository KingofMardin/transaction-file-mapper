using System.Text.RegularExpressions;
using TransactionFileMapper.Business.AreaMappers.Abstract;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;

namespace TransactionFileMapper.Business.AreaMappers.Concrete
{
    public class AccountIdentificationMapper : Mapper
    {
        private static readonly Lazy<AccountIdentificationMapper> _instance = new Lazy<AccountIdentificationMapper>(() => new AccountIdentificationMapper());
        public static AccountIdentificationMapper instance = _instance.Value;

        private List<string> patterns = new List<string>() { @"^(\d+)(-)(\d+)([A-Z]+)(\d+)$", @"^([A-Z]+)(\d+)([A-Z]+)(\d+)$" };

        public override bool Map(string value, ref MT940 file, AreaTypeEnum areaType)
        {
            bool result = false;
            foreach (var pattern in patterns)
            {
                var match = Regex.Matches(value, pattern);
                if (match.Count > 0 && match[0].Success)
                {
                    switch (pattern)
                    {
                        case (@"^(\d+)(-)(\d+)([A-Z]+)(\d+)$"):
                            break;

                        case (@"^([A-Z]+)(\d+)([A-Z]+)(\d+)$"):

                            file.AccountOwnerCode = match[0].Groups[4].Value.Trim();
                            result = true;
                            break;
                        default:

                            break;
                    }
                    break;
                }

            }
            return result;
        }
    }
}

