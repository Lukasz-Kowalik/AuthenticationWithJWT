using BackEnd.DTOs.Response;
using BackEnd.Entities;
using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public interface IUserService
    {
        UserResponse Get(string id);

        bool Create(User user);

        bool Update(string id, UserRequest user);

        bool Delete(string id);

        IEnumerable<UserResponse> Get();

        Task<string> SignInAsync(string email, string password);
    }
}