using MongoDB.Bson;

namespace BackEnd.Helpers
{
    public abstract class Document
    {
        public ObjectId Id { get; set; }
    }
}