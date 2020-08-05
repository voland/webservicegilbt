using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebServiceGilBT.Data;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Services {
    public class UserService : IUserService {
        public HttpClient _httpClient { get; }

        string BaseAddress {
            get {
#if DEBUG
                return "http://localhost:5000/";
#else
				return "http://gilbt.azurewebsites.net";
#endif
            }
        }

        public UserService(HttpClient httpClient) {

            httpClient.BaseAddress = new Uri(BaseAddress);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(User user) {
            User returnedUser = null;
            foreach (User u in UserList.users) {
                Console.WriteLine("Checking user {0}", user.EmailAddress);
                if (u.EmailAddress == user.EmailAddress)
                    if (u.Password == user.Password) {
                        returnedUser = u;
                    }
            }
            if (returnedUser != null) {
                Console.WriteLine("returned user is {0}", returnedUser.EmailAddress);
            } else {
                Console.WriteLine("returned user is null");
            }
            return await Task.FromResult(returnedUser);

        }

        public async Task<User> RegisterUserAsync(User user) {
            User returnedUser = null;
            foreach (User u in UserList.users) {
                if (u.EmailAddress == user.EmailAddress) {
                    returnedUser = u;
                    returnedUser.AdditionalInfo = $"User name {u.EmailAddress} already exists.";
                }
            }

            if (returnedUser == null) {
                if (user.Password == user.ConfirmPassword) {
                    UserList.Add(user);
                    Console.WriteLine("Adding user {0} password {1}", user.EmailAddress, user.Password);
                    returnedUser = user;
                    returnedUser.AdditionalInfo = null;
                } else {
					returnedUser = user;
                    user.AdditionalInfo = "Passwords are not equal!";
                }

            }

            return await Task.FromResult(returnedUser);
        }

    }
}
