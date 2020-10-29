using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;
using WebServiceGilBT.Services;
using WebServiceGilBT.Data;
using System.Text.Json;
using System.Linq;

namespace WebServiceGilBT.Pages {
    public partial class Index : ComponentBase, IDisposable {

        protected ScreenList screenList;

        List<Screen> listaDoWyswietlania;

        [Inject]
        protected ScreenListMySQLService ScreenListService { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        protected override void OnInitialized() {
            Debuger.PrintLn("Initialising ScreenList");
            //just temp screnlist
            screenList = ScreenListService.GetGilBTScreenList();
            Lang.LangChanged += StateHasChanged;
        }

        protected async override Task OnInitializedAsync() {
            Debuger.PrintLn("async Initialising ScreenList");

            screenList = await ScreenListService.GetGilBTScreenListAsync();
            listaDoWyswietlania = screenList.Screens;
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

        private User _u = null;
        public User user {
            set { _u = value; }
            get {
                if (_u == null) _u = new User();
                return _u;
            }
        }

        string lastOrnungClicked;

        const string cs_uid = "uid";
        const string cs_name = "name";
        const string cs_type = "type";
        const string cs_resolution = "resolution";
        const string cs_lastRequest = "lastRequest";
        const string cs_ver = "ver";
        const string cs_gmina = "gmina";

        bool orderDescending = false;
        void mkOrnungWithScreenList(string ornungBy) {
            if (lastOrnungClicked == ornungBy) {
                orderDescending = !orderDescending;
            } else {
                orderDescending = false;
            }
            switch (ornungBy) {
                case cs_uid:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.uid).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.uid).ToList();
                    }
                    lastOrnungClicked = cs_uid;
                    break;
                case cs_name:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.name).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.name).ToList();
                    }
                    lastOrnungClicked = cs_name;
                    break;
                case cs_type:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.screen_type).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.screen_type).ToList();
                    }
                    lastOrnungClicked = cs_type;
                    break;
                case cs_resolution:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.resolution()).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.resolution()).ToList();
                    }
                    lastOrnungClicked = cs_resolution;
                    break;
                case cs_lastRequest:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.last_request).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.last_request).ToList();
                    }
                    lastOrnungClicked = cs_lastRequest;
                    break;
                case cs_ver:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.firmware_ver).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.firmware_ver).ToList();
                    }
                    lastOrnungClicked = cs_ver;
                    break;
                case cs_gmina:
                    if (!orderDescending) {
                        listaDoWyswietlania = listaDoWyswietlania.OrderBy(x => x.gmina.NazwaGminy).ToList();
                    } else {
                        listaDoWyswietlania = listaDoWyswietlania.OrderByDescending(x => x.gmina.NazwaGminy).ToList();
                    }
                    lastOrnungClicked = cs_ver;
                    break;
            }
        }

        public void Dispose() {
            Lang.LangChanged -= StateHasChanged;

    }
}
