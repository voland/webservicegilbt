using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;

namespace WebServiceGilBT.Pages {
    public partial class Changeuserpassword : ComponentBase, IDisposable {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Parameter]
        public int UserIdParameter { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        UserMySQLService userService { set; get; }

        [Inject]
        Lang lng { set; get; }

        protected string OldPasswd { set; get; }
        protected string NewPasswd { set; get; }
        protected string NewPasswd2 { set; get; }

        private void NavigateHome() {
            string newurl = "/index";
            NavigationManager.NavigateTo(newurl);
        }

        protected async Task<User> GetEditedUser(int argUserId) {
            User editeduser = await userService.GetUserAsync(argUserId);
            return editeduser;
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

        protected bool OldPasswdInputDisabled { set; get; }
        protected string NoNeedPasswordMessage { set; get; }
        protected string LoginMesssage { set; get; }

        protected async override Task OnInitializedAsync() {
            lng.LangChanged += StateHasChanged;
            OldPasswdInputDisabled = false;
            logged_user = await GetLoggedUser();

            if (UserIdParameter > 0) {
                //TODO: uniknac powielania userid
                edited_user = await GetEditedUser(UserIdParameter);
                if (edited_user == null) {
                    edited_user = new User();
                    edited_user.EmailAddress = "unknown_user";
                    _edited_user.UserType = eUserType.unknown;
                    LoginMesssage = "Unknown user.";
                }
            } else {
                Console.WriteLine("logged_user is edited_user");
                edited_user = logged_user;
            }
        }

        private async Task<bool> ValidateUser() {
            if (logged_user.Password != OldPasswd) {
                /* Console.WriteLine("passwd is{0}", logged_user.Password); */
                LoginMesssage = "Provide proper current password of logged user.";
                return await Task.FromResult(true);
            }

            if ((NewPasswd != null) & (NewPasswd2 != null)) {
                if (NewPasswd != NewPasswd2) {
                    LoginMesssage = "Invalid password or new passwords are diffrent.";
                    return await Task.FromResult(true);
                }
                if (NewPasswd.Length < 6) {
                    LoginMesssage = "New password is to short.";
                    return await Task.FromResult(true);
                }

                edited_user.Password = NewPasswd;
                await userService.UpdateUserAsync(edited_user);
                if (edited_user.UserId == logged_user.UserId) {
                    await _sessionStorageService.RemoveItemAsync("loggedUser");
                    await _sessionStorageService.SetItemAsync("loggedUser", JsonSerializer.Serialize(edited_user));
                }
                LoginMesssage = "Password changed successfuly.";
            } else {
                LoginMesssage = "Provide some new password at least 6 characters.";
            }
            NavigationManager.NavigateTo($"changeuserpassword/{edited_user.UserId}");
            return await Task.FromResult(true);
        }

        public void Dispose() {
            lng.LangChanged -= StateHasChanged;
        }

        private User _edited_user = null;

        public User edited_user {
            set { _edited_user = value; }
            get {
                if (_edited_user == null) {
                    _edited_user = new User();
                    _edited_user.EmailAddress = "unknown_user";
                    _edited_user.UserType = eUserType.unknown;
                }
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
