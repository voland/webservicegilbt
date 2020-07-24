using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System;

namespace WebServiceGilBT.Shared {
    public class GilBTScreenListService : IScreenListService {

        private HttpClient httpClient;

        private static Screen _es;

        public void SetEditedScreen(Screen s){
			_es = s;
		}

        public Screen GetEditedScreen(){
			return _es;
		}

        public GilBTScreenListService(HttpClient httpClient) {
            this.httpClient = httpClient;
            Debuger.PrintLn("Adding httpClient");
#if DEBUG
            httpClient.BaseAddress = new Uri("http://localhost:5000/");
#else
            httpClient.BaseAddress = new Uri("http://gilbt.azurewebsites.net");
#endif
            Debuger.PrintLn("Added httpClient");
        }

        public ScreenList GetGilBTScreenList() {
            return new ScreenList { Screens = new List<Screen>() };
        }

        public async Task PostScreenAsync(Screen argS) {
            Debuger.PrintLn("Posted screen");
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
