using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;

namespace TransactionFileMapper.Business.AreaMappers.Abstract
{
    public interface IMapper
    {
        abstract bool Map(string value, ref MT940 file, AreaTypeEnum areaType);

    }
}
