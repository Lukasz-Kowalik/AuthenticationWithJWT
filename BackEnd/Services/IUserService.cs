using BackEnd.Entities;

namespace BackEnd.Services
{
    public interface IUserService
    {
        User Get(string id);

        bool Create(User user);

        bool Update(User user);

        bool Delete(string id);
    }
}