namespace BackEnd.Entities
{
    public class TokenSettings : ITokenSettings
    {
        public string Secret { get; init; }
        public int ExpireTimeInMinutes { get; init; }
    }
}