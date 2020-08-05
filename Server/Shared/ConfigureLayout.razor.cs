using System;
using Microsoft.AspNetCore.Components;
using WebServiceGilBT.Services;

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
            string newurl = "/index";
            NavigationManager.NavigateTo(newurl);
        }

        public void ApplyClicked() {
			Screen.from_led_screen = false;
            ScreenListService.PostScreenAsync(Screen);
        }

        public void CancelClicked() {
            NavigateHome();
        }
    }
}
