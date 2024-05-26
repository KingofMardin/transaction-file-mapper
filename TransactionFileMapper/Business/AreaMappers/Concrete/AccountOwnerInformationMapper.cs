using TransactionFileMapper.Business.AreaMappers.Abstract;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;

namespace TransactionFileMapper.Business.AreaMappers.Concrete
{
    public class AccountOwnerInformationMapper : Mapper
    {
        private static Lazy<AccountOwnerInformationMapper> _instance = new Lazy<AccountOwnerInformationMapper>(() => new AccountOwnerInformationMapper());
        public static AccountOwnerInformationMapper Instance = _instance.Value;

        public override bool Map(string value, ref MT940 file, AreaTypeEnum areaType)
        {
            file.AccountOwnerInformation = value;
            return true;
        }
    }
}
