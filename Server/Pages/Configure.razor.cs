using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages{
    public partial class Configure:ComponentBase{

	[Parameter]
	public int Uid {set; get;}

	public Screen edited_screen { set; get; }

	protected ScreenList screenList;

	[Inject]
	protected IScreenListService ScreenListService { set; get; }

	protected override void OnInitialized(){
	    Console.WriteLine("Initialising ScreenList for edited screen");
	    //just temp screnlist
	    screenList = ScreenListService.GetGilBTScreenList();
	    if ( edited_screen == null ) edited_screen = new Screen();
	    edited_screen.uid=Uid;
	    edited_screen.firmware_ver = "NULL";
	    edited_screen.name = "NULL";
	    edited_screen.screen_type = eScreenType.unknown;
	}

	protected async override Task OnInitializedAsync(){
	    Console.WriteLine("async Initialising ScreenList");
	    
	    screenList = await ScreenListService.GetGilBTScreenListAsync();
	    foreach( Screen s in screenList.Screens ){
		if ( s.uid == Uid ){
		    edited_screen = s;
		}
	    }
	}
    }
}
