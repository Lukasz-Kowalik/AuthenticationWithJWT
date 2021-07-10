namespace BackEnd.Models
{
    public interface IDatabaseSettings
    {
        string UsersCollectionName { get; init; }
        string TokensCollectionName { get; init; }
        string ConnectionString { get; init; }
        string DatabaseName { get; init; }
        string UserName { get; init; }
        string Password { get; init; }
    }
}