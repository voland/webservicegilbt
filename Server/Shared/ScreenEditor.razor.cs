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
            Console.WriteLine("Upload Firmware");
        }

        private void NavigateHome() {
            string newurl = "";
            NavigationManager.NavigateTo(newurl);
        }

        public void SubmintClicked() {
            ScreenListService.PostScreenAsync(Screen);
            NavigateHome();
        }

        public void CancelClicked() {
            NavigateHome();
        }

        public void DeleteClicked() {
            Console.WriteLine("DeleteClicked");
            ScreenListService.DeleteScreenAsync(Screen);
            NavigateHome();
        }

        public bool ShowConfirmDelete = false;
    }
}
