using MongoDB.Bson;

namespace TransactionFileMapper.Entities.Abstract
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = null;
    }
}
