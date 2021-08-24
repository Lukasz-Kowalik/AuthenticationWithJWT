using System;

namespace BackEnd.Entities
{
    public interface ITokenSettings
    {
        string Secret { get; init; }
        TimeSpan ExpireTimeLimit { get; init; }
    }
}