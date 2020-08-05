using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using WebServiceGilBT.Data;
using System.Text.Json;

namespace WebServiceGilBT.Data {
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider {

        private ISessionStorageService _sessionStorageService;

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService) {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            string serializedUser = await _sessionStorageService.GetItemAsync<string>("loggedUser");

            ClaimsIdentity identity = null;

            if (serializedUser != null) {
                User u = JsonSerializer.Deserialize<User>(serializedUser);
                Console.WriteLine("Loged as" + u.EmailAddress);
                identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, u.EmailAddress) }, "apiauth_type");

            } else {
                Console.WriteLine("no email address");
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsLogout() {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            _sessionStorageService.RemoveItemAsync("loggedUser");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsAuthenticated(User argUser) {
            var identity = new ClaimsIdentity(new[]{
                    new Claim( ClaimTypes.Name, argUser.EmailAddress)
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }

}
