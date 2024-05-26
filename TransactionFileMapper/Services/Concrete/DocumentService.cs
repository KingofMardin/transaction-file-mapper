using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Models;
using TransactionFileMapper.Repositories.Abstract;
using TransactionFileMapper.Services.Abstract;

namespace TransactionFileMapper.Services.Concrete
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository repository)
        {
            _documentRepository = repository;
        }
        public void DeleteById(ObjectId Id)
        {
            _documentRepository.DeleteById(Id);
        }

        public async Task DeleteByIdAsync(ObjectId Id)
        {
            await _documentRepository.DeleteByIdAsync(Id);
        }

        public void DeleteMany(Expression<Func<MT940, bool>> filter)
        {
            _documentRepository.DeleteMany(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<MT940, bool>> filter)
        {
            await _documentRepository.DeleteManyAsync(filter);
        }

        public void DeleteOne(Expression<Func<MT940, bool>> filter)
        {
            _documentRepository.DeleteOne(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<MT940, bool>> filter)
        {
            await _documentRepository.DeleteOneAsync(filter);
        }

        public IEnumerable<MT940> FilterBy(Expression<Func<MT940, bool>> filter)
        {
            return _documentRepository.FilterBy(filter);
        }

        public async Task<IEnumerable<MT940>> FilterByAsync(Expression<Func<MT940, bool>> filter)
        {
            var docs = await _documentRepository.FilterByAsync(filter);
            return docs;
        }

        public IEnumerable<MT940> GetAll()
        {
            return _documentRepository.GetAll();
        }

        public async Task<IEnumerable<MT940>> GetAllAsAsync()
        {
            var docs = await _documentRepository.GetAllAsAsync();
            return docs;
        }
        public List<MT940> GetDocumentsByFilter(FilterDefinition<MT940> filter)
        {
            return _documentRepository.GetDocumentsByFilter(filter).ToList();
        }

        public MT940 GetByFilter(FilterDefinition<MT940> filter)
        {
            return _documentRepository.GetByFilter(filter);
        }

        public MT940 GetById(ObjectId Id)
        {
            return _documentRepository.GetById(Id);
        }
        public GenericResultModel<GetMT940FileResponseModel> GetByDocumentId(ObjectId Id)
        {
            var filter = Builders<MT940>.Filter.Eq(
                x => x.Id, Id);

            var result = GetByFilter(filter);

            var document = new GetMT940FileResponseModel()
            {
                DocumentId = result.Id.ToString(),
                CreateDate = result.CreatedDate.ToString(),
                AccountOwnerCode = result.AccountOwnerCode,
                AccountOwnerInformation = result.AccountOwnerInformation,
                ClosingAvailableBalance = new ClosingAvailableBalanceResponseModel
                {
                    Balance = result.ClosingAvailableBalance.Balance,
                    BookDate = result.ClosingAvailableBalance.BookDate,
                    CreditDebitCode = result.ClosingAvailableBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = result.ClosingAvailableBalance.CurrencyCode,
                },
                ClosingBalance = new ClosingBalanceResponseModel
                {
                    Balance = result.ClosingBalance.Balance,
                    BookDate = result.ClosingBalance.BookDate,
                    CreditDebitCode = result.ClosingBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = result.ClosingBalance.CurrencyCode,
                },
                CompanyId = result.CompanyId.ToString(),
                ForwardAvailableBalance = new ForwardAvailableBalanceResponseModel
                {
                    Balance = result.ForwardAvailableBalance.Balance,
                    BookDate = result.ForwardAvailableBalance.BookDate,
                    CreditDebitCode = result.ForwardAvailableBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = result.ForwardAvailableBalance.CurrencyCode,
                },
                IdentifierCode = result.IdentifierCode,
                OpeningBalance = new OpeningBalanceResponseModel
                {
                    Balance = result.OpeningBalance.Balance,
                    BookDate = result.OpeningBalance.BookDate,
                    CreditDebitCode = result.OpeningBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = result.OpeningBalance.CurrencyCode,
                },
                RelatedReference = result.RelatedReference,
                SequenceNumber = result.SequenceNumber,
                StamentNumber = result.StamentNumber,
                SubsidiaryId = result.SubsidiaryId.ToString(),
                TransactionReferenceNumber = result.TransactionReferenceNumber,
                Transactions = result.Transactions.Select(x => new TransactionResponseModel
                {

                    Amount = x.Amount,
                    BookedDate = x.BookedDate,
                    CurrencyCodeLastLetter = x.CurrencyCodeLastLetter,
                    DocumentNumber = x.DocumentNumber,
                    HasAcocuntOwnerData = x.HasAcocuntOwnerData,
                    IdentificationCodeEnum = x.IdentificationCodeEnum,
                    ProcessDescription = x.ProcessDescription,
                    TransactionDate = x.TransactionDate,
                    TransactionProcessType = x.TransactionProcessType == TransactionProcessTypeEnum.D ? "D" :
                                             x.TransactionProcessType == TransactionProcessTypeEnum.C ? "C" :
                                             x.TransactionProcessType == TransactionProcessTypeEnum.RC ? "RC" : "RD",
                    TransactionType = x.TransactionType


                }).ToList(),
            };

            return new GenericResultModel<GetMT940FileResponseModel>()
            {
                Data = document,
                IsSuccess = true,
                Message = "Success"
            };
        }

        public async Task<MT940> GetByIdAsync(ObjectId Id)
        {
            return await _documentRepository.GetByIdAsync(Id);
        }

        public GenericResultModel<List<GetMT940FileResponseModel>> GetDocumentsByCompanyAndSubsidiaryId(string companyId, string subsidiaryId)
        {

            var filter = Builders<MT940>.Filter.Eq(
                x => x.CompanyId, ObjectId.Parse(companyId));

            var result = GetDocumentsByFilter(filter).Select(x => new GetMT940FileResponseModel()
            {
                DocumentId = x.Id.ToString(),
                CreateDate = x.CreatedDate.ToString(),
                AccountOwnerCode = x.AccountOwnerCode,
                AccountOwnerInformation = x.AccountOwnerInformation,
                ClosingAvailableBalance = new ClosingAvailableBalanceResponseModel
                {
                    Balance = x.ClosingAvailableBalance.Balance,
                    BookDate = x.ClosingAvailableBalance.BookDate,
                    CreditDebitCode = x.ClosingAvailableBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = x.ClosingAvailableBalance.CurrencyCode,
                },
                ClosingBalance = new ClosingBalanceResponseModel
                {
                    Balance = x.ClosingBalance.Balance,
                    BookDate = x.ClosingBalance.BookDate,
                    CreditDebitCode = x.ClosingBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = x.ClosingBalance.CurrencyCode,
                },
                CompanyId = x.CompanyId.ToString(),
                ForwardAvailableBalance = new ForwardAvailableBalanceResponseModel
                {
                    Balance = x.ForwardAvailableBalance.Balance,
                    BookDate = x.ForwardAvailableBalance.BookDate,
                    CreditDebitCode = x.ForwardAvailableBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = x.ForwardAvailableBalance.CurrencyCode,
                },
                IdentifierCode = x.IdentifierCode,
                OpeningBalance = new OpeningBalanceResponseModel
                {
                    Balance = x.OpeningBalance.Balance,
                    BookDate = x.OpeningBalance.BookDate,
                    CreditDebitCode = x.OpeningBalance.CreditDebitCode == TransactionProcessTypeEnum.C ? "C" : "D",
                    CurrencyCode = x.OpeningBalance.CurrencyCode,
                },
                RelatedReference = x.RelatedReference,
                SequenceNumber = x.SequenceNumber,
                StamentNumber = x.StamentNumber,
                SubsidiaryId = x.SubsidiaryId.ToString(),
                TransactionReferenceNumber = x.TransactionReferenceNumber,
                Transactions = x.Transactions.Select(x => new TransactionResponseModel
                {

                    Amount = x.Amount,
                    BookedDate = x.BookedDate,
                    CurrencyCodeLastLetter = x.CurrencyCodeLastLetter,
                    DocumentNumber = x.DocumentNumber,
                    HasAcocuntOwnerData = x.HasAcocuntOwnerData,
                    IdentificationCodeEnum = x.IdentificationCodeEnum,
                    ProcessDescription = x.ProcessDescription,
                    TransactionDate = x.TransactionDate,
                    TransactionProcessType = x.TransactionProcessType == TransactionProcessTypeEnum.D ? "D" :
                                             x.TransactionProcessType == TransactionProcessTypeEnum.C ? "C" :
                                             x.TransactionProcessType == TransactionProcessTypeEnum.RC ? "RC" : "RD",
                    TransactionType = x.TransactionType


                }).ToList(),
            }).ToList();

            return new GenericResultModel<List<GetMT940FileResponseModel>>()
            {
                Data = result,
                IsSuccess = true,
                Message = "Success"
            };
        }

        public void InsertMany(IEnumerable<MT940> entities)
        {
            _documentRepository.InsertMany(entities);
        }

        public async Task InsertManyAsync(IEnumerable<MT940> entities)
        {
            await _documentRepository.InsertManyAsync(entities);
        }

        public void InsertOne(MT940 entity)
        {
            _documentRepository.InsertOne(entity);            
        }

        public async Task InsertOneAsync(MT940 entity)
        {
            await _documentRepository.InsertOneAsync(entity);
        }

        public void ReplaceOne(MT940 entity, ObjectId Id)
        {
            _documentRepository.ReplaceOne(entity, Id);
        }

        public async Task ReplaceOneAsync(MT940 entity, ObjectId Id)
        {
            await _documentRepository.ReplaceOneAsync(entity, Id);
        }
    }
}
