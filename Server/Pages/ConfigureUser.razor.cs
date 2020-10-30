using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using WebServiceGilBT.Data;
using WebServiceGilBT.Services;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages {
    public partial class ConfigureUser : ComponentBase, IDisposable {
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
        GminaMySqlService gs { set; get; }

        [Inject]
        AuthenticationStateProvider asp { get; set; }

        [Parameter]
        public int UserIdParam {
            get => _UserIdParam;
            set { _UserIdParam = value; przypiszUser().Wait(); }
        }

        int _UserIdParam;
        User edited_user;

        List<Gmina> gminyUzytkownika = new List<Gmina>();

        async Task przypiszUser() {
            edited_user = await userService.GetUserAsync(_UserIdParam);
            if (edited_user != null) {
                if (edited_user.ScreenAccessList == null)
                    edited_user.ScreenAccessList = new List<ScreenAccessDescriber>();
                if (edited_user.IdGmin != null) {
                    foreach (int i in edited_user.IdGmin) {
                        Gmina g = await gs.GetGminaAsync(i);
                        gminyUzytkownika.Add(g);
                    }
                } else {
                    edited_user.IdGmin = new List<int>();
                }
            }
        }

        protected async override Task OnInitializedAsync() {
            Lang.LangChanged += StateHasChanged;
            _userlist = await userService.GetUserListAsync();
            loggeduser = await GetLoggedUser();
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

        bool showScreens { set; get; } = false;
        bool showComunes { set; get; } = false;

        void screenSelected(Screen scr) {
            showScreens = false;
            edited_user.ScreenAccessList.Add(new ScreenAccessDescriber(scr.name, scr.uid, true));
        }

        void gminaSelected(Gmina gm) {
            showComunes = false;
            gminyUzytkownika.Add(gm);
            edited_user.IdGmin.Add(gm.Id);
        }

        void onRemoveClicked(ScreenAccessDescriber sad) {
            edited_user.ScreenAccessList.Remove(sad);
        }

        void onRemoveClicked(Gmina gm) {
            gminyUzytkownika.Remove(gm);
            edited_user.IdGmin.Remove(gm.Id);
        }

        protected void NavigateToChangePassword() {
            string newurl = $"changeuserpassword/{edited_user.UserId}";
            Debuger.PrintLn($"navigating to {newurl}");
            NavigationManager.NavigateTo(newurl);
        }

        async Task onSubmit() {
            await userService.UpdateUserAsync(edited_user);
            NavigationManager.NavigateTo("/index");
        }

        async Task onDeleteClicked() {
            bool confirmed = await js.InvokeAsync<bool>("confirm", Lang.rUSureUWantDeleteThisAccount);
            if (confirmed) {
                await deleteUser();
            }
        }

        async Task deleteUser() {
            await userService.DeleteUserAsync(edited_user);
            if (loggeduser == edited_user) {
                Logout();
                NavigationManager.NavigateTo("/accountDeleted");
            } else {
                NavigationManager.NavigateTo("/users");
            }
        }

        public void Logout() {
            ((CustomAuthenticationStateProvider)asp).MarkUserAsLogout();
        }

        public void Dispose() {
            Lang.LangChanged -= StateHasChanged;
        }
    }
}
