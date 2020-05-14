using System;
using System.Collections.Generic;

namespace WebServiceGilBT.Shared{

    public class ScreenList{
	public List<Screen> Screens;
	public void Add( List<Screen> argList ){
	    if ( Screens == null ) Screens = new List<Screen>();
	    foreach ( Screen s in argList ){
		Screens.Add( s );
	    }
	}
    }
}
