using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages{
    public partial class Index:ComponentBase{

	protected ScreenList ScreenList;

	[Inject]
	protected IScreenListService ScreenListService { set; get; }

	protected override void OnInitialized(){
	    Console.WriteLine("Initialising ScreenList");
	    ScreenList = ScreenListService.GetGilBTScreenList();
	}

	protected async override Task OnInitializedAsync(){
	    Console.WriteLine("async Initialising ScreenList");
	    /* ScreenList = await ScreenListService.GetGilBTScreenListAsync(); */
	}
    }
}
