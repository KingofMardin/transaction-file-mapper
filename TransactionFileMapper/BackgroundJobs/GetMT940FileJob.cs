using Quartz;
using TransactionFileMapper.Business;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.BackgroundJobs
{
    public class GetMT940FileJob : IJob
    {
        private readonly ISubsidiaryService _subsService;
        private readonly ICompanyService _companyService;
        private readonly IDocumentService _documentService;

        public GetMT940FileJob(ISubsidiaryService subsService, ICompanyService companyService, IDocumentService documentService)
        {
            _subsService = subsService;
            _companyService = companyService;
            _documentService = documentService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Dosyayı getiren banka servisini tetikle
            var companies = _companyService.GetAll().ToList();
            var subs = _subsService.GetAll().ToList();
            var banks = subs.SelectMany(s => s.Banks);
            foreach (var company in companies)
            {

                foreach (var sub in company.Subsidiaries)
                {

                    foreach (var bank in sub.Banks)
                    {
                        var response = MT940Mapper.Instance.MapDocument(Path.Combine(Directory.GetCurrentDirectory(), "MT940 Örneği.txt"), company.Id, sub.Id);
                        if (!response.IsSuccess)
                        {
                            Console.WriteLine($"Status: {response.IsSuccess}\nMessage: {response.Message} \nData: {response.Data ?? null}");
                            break;
                        }
                        _documentService.InsertOne(response.Data);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
