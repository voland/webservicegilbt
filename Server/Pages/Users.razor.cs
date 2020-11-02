using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;
using Microsoft.JSInterop;

namespace WebServiceGilBT.Pages {
    public partial class Users : ComponentBase, IDisposable {
        public List<User> _userlist;

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Inject]
        UserMySQLService userService { set; get; }

        [Inject]
        IJSRuntime js { get; set; }

        [Inject]
        Lang lng { set; get; }

        protected async override Task OnInitializedAsync() {
            lng.LangChanged += StateHasChanged;
            _userlist = await userService.GetUserListAsync();
            loggeduser = await GetLoggedUser();
        }

        protected void NavigateToConfigureUser(User argUser) {
            string newurl = $"ConfigureUser/{argUser.UserId}";
            Debuger.PrintLn($"navigating to {newurl}");
            NavigationManager.NavigateTo(newurl);
        }

        protected async Task<User> GetLoggedUser() {
            string serializedUser = await _sessionStorageService.GetItemAsync<string>("loggedUser");
            User u = null;
            if (serializedUser != null)
                u = JsonSerializer.Deserialize<User>(serializedUser);
            if (u == null) {
                u = new User();
            }
            return u;
        }

        private User _u = null;
        public User loggeduser {
            set { _u = value; }
            get {
                if (_u == null) _u = new User();
                return _u;
            }
        }

        private int bi = 0;
        protected string item_background {
            get {
                bi++;
                if ((bi %= 2) == 1) {
                    return "my_custom_th_light";
                } else {
                    return "my_custom_th_dark";
                }
            }
        }

        async Task deleteSelectedUser(User user) {
            bool confirmed = await js.InvokeAsync<bool>("confirm", $"{lng.rUSureUWantDeleteUser} {user.EmailAddress}?");
            if (confirmed) {
                await DeleteUser(user);
            }

        }

        async Task DeleteUser(User user) {
            _userlist.Remove(user);
            await userService.DeleteUserAsync(user);
        }

        public void Dispose() {
            lng.LangChanged -= StateHasChanged;
        }
    }
}
