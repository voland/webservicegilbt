using System;
using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared{
    public partial class ScreenComponent:ComponentBase{
	[Parameter]
	public GilBTScreen Screen { set; get; }
    }
}
