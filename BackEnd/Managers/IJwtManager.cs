using BackEnd.Entities;
using System.Threading.Tasks;

namespace BackEnd.Managers
{
    public interface IJwtManager
    {
        Task<Token> SignInAsync(string email, string password);

        Token RefreshToken(string token, string refreshToken);
    }
}