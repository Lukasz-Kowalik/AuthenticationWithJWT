using BackEnd.Entities;
using BackEnd.Generics;

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
            throw new System.NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public User Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}