namespace BackEnd.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UsersCollectionName { get; init; }
        public string TokensCollectionName { get; init; }
        public string ConnectionString { get; init; }
        public string DatabaseName { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}