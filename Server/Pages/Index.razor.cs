using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages{
    public partial class Index:ComponentBase{

	protected ScreenList screenList;

	[Inject]
	protected IScreenListService ScreenListService { set; get; }

	[Inject]
	NavigationManager NavigationManager {set; get;}

	protected override void OnInitialized(){
	    Debuger.PrintLn("Initialising ScreenList");
	    //just temp screnlist
	    screenList = ScreenListService.GetGilBTScreenList();
	}

	protected async override Task OnInitializedAsync(){
	    Debuger.PrintLn("async Initialising ScreenList");
	    
	    screenList = await ScreenListService.GetGilBTScreenListAsync();
	}

	protected void NavigateToConfigureScreen( Screen argScreen){
	    string newurl = $"configure/{argScreen.uid}";
	    Debuger.PrintLn($"navigating to {newurl}");
	    NavigationManager.NavigateTo(newurl);
	}
    }
}
