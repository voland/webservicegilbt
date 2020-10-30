using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebServiceGilBT.Data;
using WebServiceGilBT.Services;

namespace WebServiceGilBT.Shared {
    public partial class PrzypiszGmineComponent : ComponentBase, IDisposable {

        bool loading { set; get; } = false;

        [Inject]
        GminaMySqlService gs { set; get; }

        protected async override Task OnInitializedAsync() {
            Lang.LangChanged += StateHasChanged;
            pelnaListaGmin = await gs.GetGminaListAsync();
        }

        public void Dispose() {
            Lang.LangChanged -= StateHasChanged;
        }

        string _filterString = "";
        string filterString {
            get => _filterString; set {
                _filterString = value;
                if (_filterString.Length > 2) filteredList = pelnaListaGmin.Where(x => x.NazwaGminy.ToLower().Contains(filterString.ToLower())).ToList();
                if (gminaWybrana) gminaWybrana = false;
            }
        }

        bool gminaWybrana = false;

        [Parameter]
        public Gmina wybranaGm { set; get; }

        List<Gmina> filteredList = new List<Gmina>();
        List<Gmina> pelnaListaGmin = new List<Gmina>();

        void wybranoGmine(Gmina gm) {
            filterString = gm.NazwaGminy;
            wybranaGm = gm;
            _ekran.IdGminy = wybranaGm.Id;
            gminaWybrana = true;
        }

        int IdGminyEkranu;

        string nazwaGminyEkranu;

        Screen _ekran;

        [Parameter]
        public Screen ekran {
            get => _ekran; set {
                _ekran = value;
                if (_ekran.IdGminy > 0) {
                    IdGminyEkranu = _ekran.IdGminy;
                    Gmina g = gs.GetGminaAsync(IdGminyEkranu).Result;
                    nazwaGminyEkranu = g.stringPodpowiedzi;
                }
            }
        }
    }
}
