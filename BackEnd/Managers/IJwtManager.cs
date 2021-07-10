using BackEnd.Entities;

namespace BackEnd.Managers
{
    public interface IJwtManager
    {
        string GenerateTocken(User user);
    }
}