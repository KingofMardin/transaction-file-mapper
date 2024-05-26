using MongoDB.Bson;
using System.ComponentModel.Design;
using System.Data.Common;
using TransactionFileMapper.Business.AreaMappers.Concrete;
using TransactionFileMapper.Entities.Concrete;
using TransactionFileMapper.Enums;
using TransactionFileMapper.Models;

namespace TransactionFileMapper.Business
{
    public class MT940Mapper
    {
        private static readonly Lazy<MT940Mapper> _instance = new Lazy<MT940Mapper>(() => new MT940Mapper());
        public static MT940Mapper Instance = _instance.Value;
        public GenericResultModel<MT940> MapDocument(string documentPath, ObjectId companyId, ObjectId subsidiaryId)
        {
            var result = new GenericResultModel<MT940>();
            result.IsSuccess = false;

            #region Comment
            //patternler alan kodlarıyla dictionary yap
            //iç içe foreach-Switch kullanımının önüne geçmek amaç
            //--------------------------------------------------
            //detect area
            //ilgili alanı parserina gönder

            //List<string> patterns = new List<string>() { "^([A-Z]+)(\\d{6})([A-Z]+)(\\d{1,},\\d+)$", "([A-Z]+)(\\d+)([A-Z]+)(\\d{1,},\\d+)$", "^(\\d{16})(d{1})(d{1})(\\d+\\.\\d{2})NTRFNONREF\\/\\/FT(\\d{10})$", "^(\\d{16})(d{1})(d{1})(\\d+\\.\\d{2})NTRFNONREF\\/\\/FT(\\d{10})$" }; 
            #endregion

            try
            {
                StreamReader reader = new StreamReader(documentPath);

                var documentText = reader.ReadToEnd();
                var documentResult = SplitDocument(documentText);            
                var file = new MT940
                {
                    TransactionReferenceNumber = documentResult.FirstOrDefault(x => x.Key.Contains(Convert.ToInt32(AreaTypeEnum.TransactionRef).ToString())).Value,
                    IdentifierCode = documentResult.FirstOrDefault(x => x.Key.Contains(Convert.ToInt32(AreaTypeEnum.AccountOwner).ToString())).Value, //["25"]; //AccountOwnerCode a göre düzenlenecek canlı veri gerekli
                    AccountOwnerCode = null,
                    StamentNumber = documentResult.FirstOrDefault(x => x.Key.Contains(Convert.ToInt32(AreaTypeEnum.Statement).ToString())).Value.Split('/')[0],
                    SequenceNumber = documentResult.FirstOrDefault(x => x.Key.Contains(Convert.ToInt32(AreaTypeEnum.Statement).ToString())).Value.Split('/')[1],
                    CompanyId = companyId,
                    SubsidiaryId = subsidiaryId

                };



                foreach (var item in documentResult)
                {

                    if (item.Key.Contains(Convert.ToInt32(AreaTypeEnum.Transaction).ToString()))
                    {
                        var mappingResult = TransactionMapper.instance.Map(item.Value, ref file, AreaTypeEnum.Transaction);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while transaction mapping";
                            result.Data = file;
                            return result;
                        }
                    }
                    if (item.Key.Contains(Convert.ToUInt32(AreaTypeEnum.AccountOwner).ToString()))
                    {
                        var mappingResult = AccountIdentificationMapper.instance.Map(item.Value, ref file, AreaTypeEnum.AccountOwner);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while account owner mapping";
                            result.Data = file;
                            return result;
                        }
                    }
                    if (item.Key.Contains(Convert.ToUInt32(AreaTypeEnum.AccountOwnerInformation).ToString()))
                    {
                        var mappingResult = AccountOwnerInformationMapper.Instance.Map(item.Value, ref file, AreaTypeEnum.AccountOwnerInformation);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while Account owner information mapping";
                            result.Data = file;
                            return result;
                        }
                    }
                    if (item.Key.Contains(Convert.ToInt32(AreaTypeEnum.OpeningBalance).ToString()))
                    {
                        var mappingResult = BalanceMapper.Instance.Map(item.Value, ref file, AreaTypeEnum.OpeningBalance);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while opening balance mapping";
                            result.Data = file;
                            return result;
                        }

                    }
                    if (item.Key.Contains(Convert.ToInt32(AreaTypeEnum.ClosingBalanceAF).ToString()))
                    {
                        var mappingResult = BalanceMapper.Instance.Map(item.Value, ref file, AreaTypeEnum.ClosingBalanceAF);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while closing available balance mapping";
                            result.Data = file;
                            return result;
                        }
                    }
                    if (item.Key.Contains(Convert.ToInt32(AreaTypeEnum.ClosingBalanceBF).ToString()))
                    {
                        var mappingResult = BalanceMapper.Instance.Map(item.Value, ref file, AreaTypeEnum.ClosingBalanceBF);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while closing balance mapping";
                            result.Data = file;
                            return result;
                        }
                    }
                    if (item.Key.Contains(Convert.ToInt32(AreaTypeEnum.ForwardAvaliableBalance).ToString()))
                    {
                        var mappingResult = BalanceMapper.Instance.Map(item.Value, ref file, AreaTypeEnum.ForwardAvaliableBalance);
                        if (!mappingResult)
                        {
                            result.IsSuccess = false;
                            result.Message = "An error occured while forwarded available balance mapping";
                            result.Data = file;
                            return result;
                        }
                    }


                }

                result.IsSuccess = true;
                result.Message = "File mapping successful";
                result.Data = file;

                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Data = null;
                return result;
            }
        }
        private List<DocumentValue> SplitDocument(string input)
        {
            List<DocumentValue> values = new List<DocumentValue>();

            string[] pairs = input.Split(':');

            for (int i = 1; i < pairs.Length; i += 2)
            {
                string key = pairs[i].Trim();
                string value = i + 1 < pairs.Length ? pairs[i + 1].Split('\\')[0].Trim() : string.Empty;
                DocumentValue val = new DocumentValue() { Key = key, Value = value };
                values.Add(val);
            }
            return values;
        }
    }
}
