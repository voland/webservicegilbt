using System;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;

namespace WebServiceGilBT.Pages {
    public partial class Configure : ComponentBase, IDisposable {

        [Inject]
        protected ScreenListMySQLService ScreenListService { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Parameter]
        public int Uid { set; get; }

        public Screen Screen { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        private void NavigateHome() {
            string newurl = "/index";
            NavigationManager.NavigateTo(newurl);
        }

        public void ApplyClicked() {
            Screen.from_led_screen = false;
            ScreenListService.UpdateScreenAsync(Screen).Wait();
        }

        public void CancelClicked() {
            NavigateHome();
        }

        protected ScreenList screenList;

        protected async override Task OnInitializedAsync() {
            user = await GetLoggedUser();
            screenList = await ScreenListService.GetGilBTScreenListAsync();
            foreach (Screen s in screenList.Screens) {
                if (s.uid == Uid) {
                    Screen = s;
                }
            }
            if (Screen == null) {
                Screen = new Screen();
                Screen.uid = Uid;
                Screen.firmware_ver = "NULL";
                Screen.name = "NULL";
                Screen.screen_type = eScreenType.unknown;
            }
            lng.LangChanged += StateHasChanged;
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

        public void Dispose() {
            lng.LangChanged -= StateHasChanged;
        }

        private User _u = null;

        public User user {
            set { _u = value; }
            get {
                if (_u == null) _u = new User();
                return _u;
            }
        }
    }
}
