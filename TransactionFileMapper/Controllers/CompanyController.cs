using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Models;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet(nameof(GetCompaniesWithSubs))]
        public IActionResult GetCompaniesWithSubs()
        {
            var data = _companyService.GetAll().ToList();

            var response = new GenericResultModel<List<CompanyResponseModel>>()
            {
                IsSuccess = data != null ? true : false,
                Data = data.Select(x => new CompanyResponseModel
                {
                    Id = x.Id.ToString(),
                    UpdatedDate = x.UpdatedDate,
                    CompanyName = x.CompanyName,
                    Subsidiaries = x.Subsidiaries.Select(t => new SubsidiaryResponseModel
                    {
                        Id = t.Id.ToString(),
                        CreatedDate = t.CreatedDate,
                        SubsidiaryName = t.SubsidiaryName,
                        UpdatedDate = t.UpdatedDate
                    }).ToList(),
                    CreatedDate = x.CreatedDate
                }).ToList(),
                Message = data == null ? "Error" : "Success",

            };
            return response.IsSuccess == true ? Ok(response) : BadRequest(response);
        }
        
    }
}
