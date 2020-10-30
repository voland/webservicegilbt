using System;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Collections.Generic;
using WebServiceGilBT.Services;
using System.Net;
using System.Text.Json;
using WebServiceGilBT.Data;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;

namespace WebServiceGilBT.Shared {
    public partial class ScreenEditor : ComponentBase, IDisposable {
        [Inject]
        protected ScreenListMySQLService ScreenListService { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Inject]
        IJSRuntime js { set; get; }

        [Inject]
        GminaMySqlService gs { set; get; }

        [Parameter]
        public Screen Screen { set; get; }

        [Parameter]
        public User loggeduser { set; get; }

        public List<string> FirmwareList {
            get {
                List<string> _fl = new List<string>();
                string[] fl = Directory.GetFiles("Firmwares");
                _fl.Add("NULL");
                foreach (string s in fl) {
                    string s2 = s.Remove(0, "Firmwares/".Length);
                    if (Screen.screen_type == eScreenType.mono) {
                        if (s2.Contains(".bin")) {
                            s2 = s2.Remove(s2.Length - 4);
                            _fl.Add(s2);
                        }
                    }
                    if (Screen.screen_type == eScreenType.rgb) {
                        if (s2.Contains(".frm")) {
                            s2 = s2.Remove(s2.Length - 4);
                            _fl.Add(s2);
                        }
                    }
                }
                return _fl;
            }
        }

        public void UploadFirmwareClicked() {
            Debuger.PrintLn("Upload Firmware");
            foreach (string s in FirmwareList) {
                Debuger.PrintLn(s);
            }
        }

        private void NavigateHome() {
            string newurl = "/index";
            NavigationManager.NavigateTo(newurl);
        }

        public async Task DeleteClicked() {
            bool confirmed = await js.InvokeAsync<bool>("confirm", Lang.confirmDeleteFromDB);
            if (confirmed) {
                ScreenListService.DeleteScreenAsync(Screen).Wait();
                NavigateHome();
            }
        }

        private void HandleValidSubmit() {
            Debuger.PrintLn("OnValidSubmit");
        }

        public bool ShowConfirmDelete = false;

        private string url = "https://api.syngeos.pl/api/public/data/device/{0}";
        private const string unknowncity = "unknown city";
        private string _city = unknowncity;
        private Device d = null;
        int _UnifySensorId;
        int UnifySensorId {
            get => _UnifySensorId; set {
                if (value != 0 && value != _UnifySensorId) {

                    _UnifySensorId = value;
                    try {
                        using (WebClient wc = new WebClient()) {
                            var json = wc.DownloadString(string.Format(url, value));
                            d = JsonSerializer.Deserialize<Device>(json);
                            if (d != null) {
                                _city = d.city;
                            }
                        }
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                        _city = Lang.unkownCity;
                        //  text = "unknown device";
                    }
                } else { _UnifySensorId = 0; }
            }
        }

        void confirmUnifiedIdx() {
            Screen.pres.UnifiedIdx = _UnifySensorId;
            if (_UnifySensorId > 0) {
                foreach (Page p in Screen.pres.pages) {
                    foreach (PageElement pe in p.elements) {
                        if (pe.type.ToString().ToLower().Contains("sensor")) {
                            pe.idx = _UnifySensorId;
                        }
                    }
                }
            }
        }

        Gmina gmina;

        string nazwaGminyEkranu { get { if (gmina != null) return gmina.stringPodpowiedzi; return ""; } }

        bool pokaWyborGminy = false;
        protected override void OnInitialized() {
            Lang.LangChanged += StateHasChanged;
            base.OnInitialized();
            if (Screen != null) gmina = gs.GetGminaAsync(Screen.IdGminy).Result;
        }

        public void Dispose() {
            Lang.LangChanged -= StateHasChanged;
        }

        bool pokaWczytywanieTamplatow = false;
        bool _pokaPreview = false;
        bool pokaPreview {
            get => _pokaPreview; set {
                _pokaPreview = value;
                if (pokaPreview) {
                } else {
                    if (ps != null) ps.rysowanieWToku = false;
                }
            }
        }

        BECanvasComponent _canvasReference = null;
        Canvas2DContext _outputCanvasContext;
        PreviewService ps;
        int skalaCanvasWzgledemEkranu = 4;

        async void onPokaPreview() {
            if (_outputCanvasContext != null) {
                try {
                    ps = new PreviewService(_outputCanvasContext, Screen.pres, _canvasReference, skalaCanvasWzgledemEkranu);
                    await ps.drawAllPages();
                } catch { }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (pokaPreview) {
                if (true) {
                    _outputCanvasContext = await _canvasReference.CreateCanvas2DAsync();
                    await _outputCanvasContext.SetTextBaselineAsync(TextBaseline.Top);
                }
                if (_outputCanvasContext != null) {
                    onPokaPreview();
                }
            }
        }

        public void templatZostalWybrany(PresTemplate tm) {
            pokaWczytywanieTamplatow = false;
            Screen.pres = tm.prezentacja;
        }

    }
}
