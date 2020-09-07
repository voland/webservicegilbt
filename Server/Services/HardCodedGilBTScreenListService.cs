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
            httpClient.BaseAddress = new Uri(BaseAddress);
        }

        public ScreenList GetGilBTScreenList() {
            return new ScreenList { Screens = new List<Screen>() };
        }

        public async Task PostScreenAsync(Screen argS) {
			argS.ActualiseLastRequestTime();
            argS.from_led_screen = false;
            await httpClient.PostJsonAsync("/api/screens/postscreen", argS);
        }

        public async Task DeleteScreenAsync(Screen argS) {
            int uid = argS.uid;
            await httpClient.DeleteAsync($"/api/screens/deletescreen/{uid}");
        }

        public async Task<ScreenList> GetGilBTScreenListAsync() {
            List<Screen> sl = await httpClient.GetJsonAsync<List<Screen>>("/api/screens/getscreenlist");
            ScreenList screenList = new ScreenList();
            screenList.Add(sl);
            return screenList;
        }

        public async Task<Screen> GetGilBTScreenAsync(int uid) {
            //throw new NotImplementedException();
            return await Task.FromResult<Screen>(null);
        }

        public async Task UpdateScreenAsync(Screen argS) {
            await PostScreenAsync(argS);
        }

        public async Task UpdateLastRequestTime(Screen argS){
            //robi nic bo ta klasa jest do usuniecia
            //throw new NotImplementedException();
			await Task.CompletedTask;
		}
    }
}
