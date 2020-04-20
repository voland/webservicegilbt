using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages{
    public partial class Index:ComponentBase{
	protected GilBTScreenList ScreenList = new GilBTScreenList();
	protected override void OnInitialized(){
	    ScreenList.Screens = new List<GilBTScreen>() {
		new GilBTScreen() { ID= 0, Name="Poznan",},
		new GilBTScreen() { ID= 1, Name="Steszew",},
		new GilBTScreen() { ID= 2, Name="lubon",},
	    };
	}
    }
}

