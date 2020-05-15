using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System;

namespace WebServiceGilBT.Shared {
    public class GilBTScreenListService :IScreenListService  {

	private HttpClient httpClient;

	/* public GilBTScreenListService( HttpClient httpClient ){ */
	/* this.httpClient = httpClient; */
	/* } */

	public GilBTScreenListService( HttpClient httpClient ){
	    this.httpClient = httpClient;
	    Console.WriteLine("Added httpClient");
	}

	public ScreenList GetGilBTScreenList() {
	    return new ScreenList { 
		Screens = new List<Screen>{
		    new Screen() { uid= 0, name="Komorniki",},
			new Screen() { uid= 1, name="Kościan",},
			new Screen() { uid= 2, name="Lubon",},
			new Screen() { uid= 3, name="Grodzisk",},
			new Screen() { uid= 4, name="Łódź",},
			new Screen() { uid= 5, name="Stęszew",},
		} 
	    };
	}

	public async Task PostScreenAsync( Screen argS ){
	    Console.WriteLine("Posted screen");
	    await httpClient.PostJsonAsync("http://localhost:5000/api/screens/postscreen", argS);
	}

	public async Task DeleteScreenAsync( Screen argS ){
	    int uid = argS.uid;
	    Console.WriteLine("Deleting screen uid {0}", uid);
	    await httpClient.DeleteAsync($"http://localhost:5000/api/screens/deletescreen/{uid}");
	}

	public async Task<ScreenList> GetGilBTScreenListAsync() {
	    List<Screen> sl = await httpClient.GetJsonAsync<List<Screen>>("http://localhost:5000/api/screens/getscreenlist");
	    ScreenList screenList = new ScreenList();
	    screenList.Add( sl );
	    return screenList;
	}
    }
}
