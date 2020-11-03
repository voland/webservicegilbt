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

        [Inject]
        Lang lng { set; get; }

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
            bool confirmed = await js.InvokeAsync<bool>("confirm", lng.confirmDeleteFromDB);
            if (confirmed) {
                ScreenListService.DeleteScreenAsync(Screen).Wait();
                NavigateHome();
            }
        }

        private void HandleValidSubmit() {
            Debuger.PrintLn("OnValidSubmit");
        }

        public bool ShowConfirmDelete = false;

        private const string unknowncity = "unknown city";
        private string _city {
            get {
                if (UnifySensorId == 0) return "Wyłączony";
                try {
                    Device d = DeviceList.GetDeviceById(UnifySensorId);
                    return d.city;
                } catch {
                }
                return unknowncity;
            }
        }

        private Device d = null;

        int UnifySensorId {
            set { Screen.pres.UnifiedIdx = value; }
            get { return Screen.pres.UnifiedIdx; }
        }

        void UpdateUnifiedIdx() {
            if (UnifySensorId > 0) {
                foreach (Page p in Screen.pres.pages) {
                    foreach (PageElement pe in p.elements) {
                        if (pe.type.ToString().ToLower().Contains("sensor")) {
                            pe.idx = UnifySensorId;
                        }
                    }
                }
            }
        }

        Gmina gmina;

        string nazwaGminyEkranu {
            get {
                if (gmina != null)
                    return gmina.stringPodpowiedzi;
                return "";
            }
        }

        bool pokaWyborGminy { set; get; } = false;

        protected override void OnInitialized() {
            lng.LangChanged += StateHasChanged;
            base.OnInitialized();
            if (Screen != null)
                gmina = gs.GetGminaAsync(Screen.IdGminy).Result;
        }

        public void Dispose() {
            lng.LangChanged -= StateHasChanged;
        }

        bool pokaWczytywanieTamplatow { set; get; } = false;
        BECanvasComponent _canvasReference = null;
        Canvas2DContext _outputCanvasContext;
        PreviewService _preview_service;
        int skalaCanvasWzgledemEkranu = 4;

        async void PokaPreview() {
            if (_outputCanvasContext != null) {
                try {
                    _preview_service = new PreviewService(_outputCanvasContext, Screen.pres, _canvasReference, skalaCanvasWzgledemEkranu);
                    await _preview_service.drawAllPages();
                } catch { }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            _outputCanvasContext = await _canvasReference.CreateCanvas2DAsync();
            await _outputCanvasContext.SetTextBaselineAsync(TextBaseline.Top);
            if (_outputCanvasContext != null) {
                if (firstRender)
                    PokaPreview();
            }
        }

        public void SelectTemplate(PresTemplate tm) {
            int temp_unified_idx = UnifySensorId;
            pokaWczytywanieTamplatow = false;
            Screen.pres = tm.prezentacja;
            UnifySensorId = temp_unified_idx;
            UpdateUnifiedIdx();
            _preview_service?.SetPresentationToPlay(Screen.pres);
        }

        private void OnGminaSelected(Gmina argGmina) {
            Console.WriteLine("Gmina changed to {0}", argGmina.NazwaGminy);
            gmina = argGmina;
            pokaWyborGminy = false;
        }

    }
}
