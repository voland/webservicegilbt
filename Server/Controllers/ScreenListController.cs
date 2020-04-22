using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Controller
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ScreensController : ControllerBase{

	public static List<Screen>  ScreenList = new List<Screen>{ 
	    new Screen() { UID= 0, Name="Komorniki",},
	    new Screen() { UID= 1, Name="Kościan", FirmwareBin = new byte[] { 1,2,3 ,4 ,5}},
	    new Screen() { UID= 2, Name="Lubon",},
	    new Screen() { UID= 3, Name="Grodzisk",},
	    new Screen() { UID= 4, Name="Łódź",},
	    new Screen() { UID= 5, Name="Stęszew",},
	}; 

	[HttpGet]
	public IQueryable<Screen> GetScreenList(){
	    return ScreenList.AsQueryable();
	}

	[HttpGet("{uid:int}")]
	public Screen GetScreen(int uid){
	    Console.WriteLine($"Getting {uid}");
	    Screen temp = ScreenList.Single( screen => screen.UID == uid);
	    if ( temp != null ){
		temp.LastRequest = DateTime.Now;
		return temp;
	    }else{
		return null;
	    }
	}

	[HttpPost("AddScreen")]
	public IActionResult PostScreen([FromBody] Screen argScreen){
	    if ( argScreen != null ){
		Screen temp = ScreenList.Single(  screen => argScreen.UID == screen.UID );
		if ( temp!= null){
		    return Created($"Already exists.", null);
		}else{
		    ScreenList.Add(argScreen);
		    return Created($"Success, added.", null);
		}
	    }else{
		return Created($"Fail.", null);
	    }
	}
    }
}
