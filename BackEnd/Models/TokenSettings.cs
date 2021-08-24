using System;

namespace BackEnd.Entities
{
    public class TokenSettings : ITokenSettings
    {
        public string Secret { get; init; }
        public TimeSpan ExpireTimeLimit { get; init; }
    }
}