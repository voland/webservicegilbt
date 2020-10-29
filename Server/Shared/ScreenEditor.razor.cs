using System;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Collections.Generic;
using WebServiceGilBT.Services;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;

namespace WebServiceGilBT.Shared {
    public partial class ScreenEditor : ComponentBase {
        [Inject]
        protected ScreenListMySQLService ScreenListService { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Parameter]
        public Screen Screen { set; get; }

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

        public void DeleteClicked() {
            Debuger.PrintLn("DeleteClicked");
            ScreenListService.DeleteScreenAsync(Screen).Wait();
            NavigateHome();
        }

        private void HandleValidSubmit() {
            Debuger.PrintLn("OnValidSubmit");
        }

        public bool ShowConfirmDelete = false;
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
    }
}
