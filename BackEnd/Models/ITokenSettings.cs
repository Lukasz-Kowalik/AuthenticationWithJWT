namespace BackEnd.Entities
{
    public interface ITokenSettings
    {
        string Secret { get; init; }
        int ExpireTimeInMinutes { get; init; }
    }
}