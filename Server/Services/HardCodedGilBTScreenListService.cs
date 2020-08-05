using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Services {
    public class GilBTScreenListService : IScreenListService {

        private HttpClient httpClient;

        string BaseAddress {
            get {
#if DEBUG
                return "http://localhost:5000/";
#else
				return "http://gilbt.azurewebsites.net";
#endif
            }
        }

        public GilBTScreenListService(HttpClient httpClient) {
            this.httpClient = httpClient;
            Debuger.PrintLn("Adding httpClient");
            httpClient.BaseAddress = new Uri(BaseAddress);
            Debuger.PrintLn("Added httpClient");
        }

        public ScreenList GetGilBTScreenList() {
            return new ScreenList { Screens = new List<Screen>() };
        }

        public async Task PostScreenAsync(Screen argS) {
            Debuger.PrintLn("Posted screen");
            argS.from_led_screen = false;
            await httpClient.PostJsonAsync("/api/screens/postscreen", argS);
        }

        public async Task DeleteScreenAsync(Screen argS) {
            int uid = argS.uid;
            Debuger.PrintLn("Deleting screen uid {0}", uid);
            await httpClient.DeleteAsync($"/api/screens/deletescreen/{uid}");
        }

        public async Task<ScreenList> GetGilBTScreenListAsync() {
            List<Screen> sl = await httpClient.GetJsonAsync<List<Screen>>("/api/screens/getscreenlist");
            ScreenList screenList = new ScreenList();
            screenList.Add(sl);
            return screenList;
        }
    }
}
