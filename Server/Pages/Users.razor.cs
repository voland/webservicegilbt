using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;

namespace WebServiceGilBT.Pages {
    public partial class Users : ComponentBase {
        public List<User> _userlist;

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Inject]
        IUserService userService { set; get; }

        protected async override Task OnInitializedAsync() {
            _userlist = await userService.GetUserListAsync();
            loggeduser = await GetLoggedUser();
        }

        protected void NavigateToConfigureUser(User argUser) {
            string newurl = $"configureuser/{argUser.EmailAddress}";
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

		protected int _tab_idx=1;
		protected int tab_idx{
			get {
				return _tab_idx++;
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

    }
}
