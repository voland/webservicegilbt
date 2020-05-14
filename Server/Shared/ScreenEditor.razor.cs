using System;
using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared{
    public partial class ScreenEditor:ComponentBase{
	[Inject]
	protected IScreenListService ScreenListService { set; get; }

	[Inject]
	NavigationManager NavigationManager {set; get;}

	[Parameter]
	public Screen Screen { set; get; }

	public void UploadFirmwareClicked(){
	    Console.WriteLine("Upload Firmware");
	}

	public void SubmintClicked(){
	    ScreenListService.PostScreenAsync( Screen );

	    string newurl = "http://localhost:5000/";
	    NavigationManager.NavigateTo(newurl);
	}
    }
}
