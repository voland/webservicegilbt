using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;

namespace WebServiceGilBT.Pages {
    public partial class Index : ComponentBase {

        protected ScreenList screenList;

        [Inject]
        protected IScreenListService ScreenListService { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        protected override void OnInitialized() {
            Debuger.PrintLn("Initialising ScreenList");
            //just temp screnlist
            screenList = ScreenListService.GetGilBTScreenList();
        }

        protected async override Task OnInitializedAsync() {
            Debuger.PrintLn("async Initialising ScreenList");

            screenList = await ScreenListService.GetGilBTScreenListAsync();
            user = await GetLoggedUser();
        }

        protected void NavigateToConfigureScreen(Screen argScreen) {
            string newurl = $"configure/{argScreen.uid}";
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
        public User user {
            set { _u = value; }
            get {
                if (_u == null) _u = new User();
                return _u;
            }
        }
    }
}
