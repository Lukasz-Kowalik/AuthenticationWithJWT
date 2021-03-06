using BackEnd.DTOs.Response;
using BackEnd.Entities;
using BackEnd.Generics;
using BackEnd.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _userRepository;

        public UserService(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Create(User user)
        {
            try
            {
                _userRepository.Insert(user);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                _userRepository.DeleteOne(x => x.Id == new ObjectId(id));
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public UserResponse Get(string id)
        {
            var user = _userRepository.GetById(id);
            return new UserResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };
        }

        public bool Update(string id, UserRequest request)
        {
            var user = _userRepository.GetById(id);
            user.Email = request.Email;
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Password = request.Password;

            try
            {
                _userRepository.Update(user);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public IEnumerable<UserResponse> Get()
        {
            var users = _userRepository.GetAll();
            return users.Select(x => new UserResponse
            {
                Id = x.Id.ToString(),
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname
            });
        }
    }
}