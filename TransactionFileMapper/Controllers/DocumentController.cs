using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Models;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpPost(nameof(GetDocumentsByCompanyAndSubsId))]
        public IActionResult GetDocumentsByCompanyAndSubsId([FromBody] GetDocumentModel getDocumentModel)
        {
            var result = _documentService.GetDocumentsByCompanyAndSubsidiaryId(getDocumentModel.CompanyId, getDocumentModel.SubsidiaryId);
            
            return Ok(result);
        }
        [HttpGet(nameof(GetDocumentDetails))]
        public IActionResult GetDocumentDetails(string documentId)
        {
            return Ok(_documentService.GetByDocumentId(ObjectId.Parse(documentId)));
            //GenericResultModel<GetMT940FileResponseModel> GetByDocumentId(ObjectId Id)
        }
        public class GetDocumentModel
        {
            public string CompanyId { get; set; }
            public string SubsidiaryId { get; set; }
        }
    }
}
