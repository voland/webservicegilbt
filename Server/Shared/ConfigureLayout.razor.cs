
using System;
using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared {
    public partial class ConfigureLayout : LayoutComponentBase {
        [Inject]
        protected IScreenListService ScreenListService { set; get; }

        [Inject]
        NavigationManager NavigationManager { set; get; }

        public Screen Screen {
            set {
                ScreenListService.SetEditedScreen(value);
            }
            get {
                return ScreenListService.GetEditedScreen();
            }
        }

        private void NavigateHome() {
            string newurl = "";
            NavigationManager.NavigateTo(newurl);
        }

        public void ApplyClicked() {
            ScreenListService.PostScreenAsync(Screen);
        }

        public void CancelClicked() {
            NavigateHome();
        }
    }
}
