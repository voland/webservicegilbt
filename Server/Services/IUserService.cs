using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServiceGilBT.Data;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Services {
    public interface IUserService {
        public Task<User> LoginAsync(User user);
        public Task<User> RegisterUserAsync(User user);
        public Task UpdateUserAsync(User argUser);
        public Task<List<User>> GetUserListAsync();
        public Task AddUserAsync(User argS);
        public Task<User> GetUserAsync(int uid);
        public Task<User> GetUserWithNameAndPasswordAsync(string email, string password);
        public Task<User> GetUserAsync(string email);
    }
}
