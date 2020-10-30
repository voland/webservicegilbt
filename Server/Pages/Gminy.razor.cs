using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebServiceGilBT.Data;
using WebServiceGilBT.Services;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages {
    public partial class Gminy : ComponentBase, IDisposable {
        public List<User> _userlist;

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Inject]
        UserMySQLService userService { set; get; }

        [Inject]
        GminaMySqlService gs { set; get; }

        [Inject]
        ScreenListMySQLService screenService { set; get; }

        protected async override Task OnInitializedAsync() {
            Lang.LangChanged += StateHasChanged;
            _userlist = await userService.GetUserListAsync();
            loggeduser = await GetLoggedUser();
            listaCT = await przygotujListeCrossTableGmiy();
        }

        List<Gmina> listaCT;

        async Task<List<Gmina>> przygotujListeCrossTableGmiy() {
            List<Gmina> listaWszytkichGminEver = await gs.GetGminaListAsync();
            List<Gmina> lctg = new List<Gmina>();
            List<Screen> scrLst = (await screenService.GetGilBTScreenListAsync()).Screens;
            foreach (User u in _userlist) {
                if (u.IdGmin != null) {
                    foreach (int i in u.IdGmin) {
                        Gmina g = listaWszytkichGminEver.FirstOrDefault(x => x.Id == i);
                        Gmina tg = lctg.FirstOrDefault(x => x.Id == g.Id);
                        if (tg != null) {
                            tg.uzytkownicy.Add(u);
                        } else {
                            g.uzytkownicy.Add(u);
                            lctg.Add(g);
                        }
                    }
                }
            }
            foreach (Screen s in scrLst) {
                Gmina g = listaWszytkichGminEver.FirstOrDefault(x => x.Id == s.IdGminy);
                if (g != null) {
                    Gmina tg = lctg.FirstOrDefault(x => x.Id == g.Id);
                    if (tg != null) {
                        tg.ekrany.Add(s);
                    } else {
                        g.ekrany.Add(s);
                        lctg.Add(g);
                    }
                }
            }
            return lctg;
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

        int wybranyWiersz;

        string _filterString = "";
        string filterString {
            get => _filterString; set {
                _filterString = value;
                if (_filterString.Length > 2) filteredList = pelnaListaGmin.Where(x => x.NazwaGminy.ToLower().Contains(filterString.ToLower())).ToList();
                if (gminaWybrana) gminaWybrana = false;
            }
        }

        bool gminaWybrana = false;

        Gmina wybranaGm;

        List<Gmina> filteredList = new List<Gmina>();

        List<Gmina> pelnaListaGmin = new List<Gmina>();

        void wybranoGmine(Gmina gm) {
            filterString = gm.NazwaGminy;
            wybranaGm = gm;
            gminaWybrana = true;
        }

        public void Dispose() {
            Lang.LangChanged -= StateHasChanged;
        }

        string styleBGColorString {
            get {
                if (rowNo % 2 == 0) {
                    return string.Empty;
                } else {
                    return "background-color:#ddd";
                }
            }
        }

        string styleBorderString {
            get {
                if (rowNo != wybranyWiersz) {
                    return string.Empty;
                } else {
                    return "; border: solid; border-width: 2px ";
                }
            }
        }

        int rowNo;
        int si;
        int ui;

        Gmina _selectedItem;
        Gmina selectedItem {
            get { return _selectedItem; }
            set {
                if (selectedItem != value) {
                    _selectedItem = value;
                    si = value.ekrany.Count;
                    ui = value.uzytkownicy.Count;
                    wybranyWiersz = listaCT.IndexOf(value) + 1;
                } else {
                    _selectedItem = null;
                    wybranyWiersz = -1;
                }
            }
        }

    }
}
