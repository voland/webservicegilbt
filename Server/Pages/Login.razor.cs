using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

namespace WebServiceGilBT.Pages {
    public partial class Login : ComponentBase {


        private User user;
        public string LoginMesssage { get; set; }
        /* @* ClaimsPrincipal claimsPrincipal; *@ */

        /* @* [CascadingParameter] *@ */
        /* @* private Task<AuthenticationState> authenticationStateTask { get; set; } *@ */

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorage { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        UserMySQLService userService { set; get; }

        protected override void OnInitialized() {
            user = new User();
        }

        private async Task<bool> ValidateUser() {
            User returnedUser = await userService.LoginAsync(user);
            if (returnedUser != null) {

                await _sessionStorage.SetItemAsync("loggedUser", JsonSerializer.Serialize(returnedUser));

                ((CustomAuthenticationStateProvider)AuthenticationStateProvider).
                    MarkUserAsAuthenticated(user);
                NavigationManager.NavigateTo("/index");
            } else {
                LoginMesssage = "Invalid username or password";
            }

            return await Task.FromResult(true);
        }
    }
}
