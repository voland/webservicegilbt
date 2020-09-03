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
        public Task<List<User>> GetUserListAsync();
        Task AddUserAsync(User argS);
        Task<User> GetSpecificUser(int uid);
        Task<User> GetUserWithSpecificNameAndPassword(string email, string password);
        Task<User> GetSpecificUser(string email);
    }
}
