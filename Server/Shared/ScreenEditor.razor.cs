using System;
using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared {
    public partial class ScreenEditor : ComponentBase {
        [Inject]
        protected IScreenListService ScreenListService { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        [Parameter]
        public Screen Screen { set; get; }

        public void UploadFirmwareClicked() {
            Debuger.PrintLn("Upload Firmware");
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
