using BackEnd.Attributes;
using System;

namespace BackEnd.Entities
{
    [BsonCollection("Tokens")]
    public class Token
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public bool Invalidated { get; set; }
    }
}