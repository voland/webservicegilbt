using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;

namespace WebServiceGilBT.Data {
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider {

        private ISessionStorageService _sessionStorageService;

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService) {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            var emailAddress = await _sessionStorageService.GetItemAsync<string>("emailAddress");

            ClaimsIdentity identity = null;

            if (emailAddress != null) {
                Console.WriteLine(emailAddress);
                identity = new ClaimsIdentity(new[]{
                    new Claim( ClaimTypes.Name, emailAddress)
            }, "apiauth_type");

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
            _sessionStorageService.RemoveItemAsync("emailAddress");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsAuthenticated(string emailAddress) {
            var identity = new ClaimsIdentity(new[]{
                    new Claim( ClaimTypes.Name, emailAddress)
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }

}
