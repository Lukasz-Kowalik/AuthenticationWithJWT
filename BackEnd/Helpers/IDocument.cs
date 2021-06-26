using MongoDB.Bson;

namespace BackEnd.Helpers
{
    public interface IDocument
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }
    }
}