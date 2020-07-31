using System;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Collections.Generic;

namespace WebServiceGilBT.Shared {
    public partial class ScreenEditor : ComponentBase {
        [Inject]
        protected IScreenListService ScreenListService { set; get; }

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
            string newurl = "";
            NavigationManager.NavigateTo(newurl);
        }

        public void DeleteClicked() {
            Debuger.PrintLn("DeleteClicked");
            ScreenListService.DeleteScreenAsync(Screen);
            NavigateHome();
        }

        private void HandleValidSubmit() {
            Debuger.PrintLn("OnValidSubmit");
        }

        public bool ShowConfirmDelete = false;
    }
}
