using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebServiceGilBT.Pages {
    public partial class SignUp : ComponentBase {
        private User user;
        public string LoginMesssage { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorage { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        IUserService userService { set; get; }

        protected override Task OnInitializedAsync() {
            user = new User();
            return base.OnInitializedAsync();
        }


        private async Task<bool> RegisterUser() {
            //assume that user is valid
            var returnedUser = await userService.RegisterUserAsync(user);

            if (returnedUser != null) {
                if (returnedUser.AdditionalInfo != null) {
					LoginMesssage = returnedUser.AdditionalInfo;
                } else {
					LoginMesssage = "User created sucessfully, now you can click \"Alrady an user?\" and login.";
                }
            } else {
                LoginMesssage = "Invalid username or password";
            }

            return await Task.FromResult(true);
        }
    }
}
