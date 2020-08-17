using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;

namespace WebServiceGilBT.Pages {
    public partial class Changeuserpassword : ComponentBase {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Parameter]
        public int UserIdParameter { set; get; }


        [Inject]
        NavigationManager NavigationManager { set; get; }


        protected string OldPasswd { set; get; }
        protected string NewPasswd { set; get; }
        protected string NewPasswd2 { set; get; }

        private void NavigateHome() {
            string newurl = "/index";
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

        protected async override Task OnInitializedAsync() {
            logged_user = await GetLoggedUser();
            if (UserIdParameter > 0) {
				//TODO: add searching for edited user
				//TODO: uniknac powielania userid
				Console.WriteLine("new user");
				edited_user = new User();
			}else{
				Console.WriteLine("logged_user is edited_user");
				edited_user = logged_user;
			}
        }

        protected string LoginMesssage { set; get; }

        private async Task<bool> ValidateUser() {
            if (edited_user.Password != OldPasswd) {
                LoginMesssage = "Provide proper old password.";
                return await Task.FromResult(true);
            }
            if (NewPasswd != NewPasswd2) {
                LoginMesssage = "Invalid password or new passwords are diffrent.";
                return await Task.FromResult(true);
            }
            if (NewPasswd.Length < 6) {
                LoginMesssage = "New password is to short.";
				return await Task.FromResult(true);
            }

            LoginMesssage = "Password changed successfuly.";
            return await Task.FromResult(true);
        }

        private User _edited_user = null;

		public User edited_user{
            set { _edited_user = value; }
            get {
                if (_edited_user == null) _edited_user = new User();
                return _edited_user;
            }
		}

        private User _logged_user = null;

        public User logged_user {
            set { _logged_user = value; }
            get {
                if (_logged_user == null) _logged_user = new User();
                return _logged_user;
            }
        }
    }
}
