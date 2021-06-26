using BackEnd.Helpers;
using MongoDB.Bson;

namespace BackEnd.Entities
{
    [BsonCollection("Users")]
    public class User : IDocument
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public Token Token { get; set; }
    }
}