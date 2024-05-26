using MongoDB.Bson;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Models;

namespace TransactionFileMapper.Services.Abstract
{
    public interface IDocumentService : IGenericService<MT940>
    {
        GenericResultModel<List<GetMT940FileResponseModel>> GetDocumentsByCompanyAndSubsidiaryId(string companyId, string subsidiaryId);
        GenericResultModel<GetMT940FileResponseModel> GetByDocumentId(ObjectId Id);

    }
}
