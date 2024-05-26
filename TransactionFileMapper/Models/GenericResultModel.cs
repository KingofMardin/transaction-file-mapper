namespace TransactionFileMapper.Models
{
    public sealed class GenericResultModel<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}
