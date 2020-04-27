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
	    new Screen() { uid= 0, name="Komorniki",},
	    new Screen() { uid= 1, name="Kościan", },
	    new Screen() { uid= 2, name="Lubon",},
	    new Screen() { uid= 3, name="Grodzisk",},
	    new Screen() { uid= 4, name="Łódź",},
	}; 

	[HttpGet]
	public IQueryable<Screen> GetScreenList(){
	    return ScreenList.AsQueryable();
	}

	[HttpGet("{uid:int}")]
	public Screen GetScreen(int uid){
	    Console.WriteLine($"Getting {uid}");
	    Screen temp = null;
	    foreach ( Screen s in ScreenList ) if ( s.uid == uid) temp = s;
	    if ( temp != null ){
		temp.last_request = DateTime.Now;
		return temp;
	    }else{
		return new Screen { name = "null", uid = 0 };
	    }
	}

	[HttpPost]
	public IActionResult PostScreen([FromBody] Screen argScreen){
	    Console.WriteLine("Posting Screen {0}.", argScreen.uid);
	    if ( argScreen != null ){
		argScreen.last_request = DateTime.Now;
		Screen temp = null;
		foreach ( Screen s in ScreenList ) if ( s.uid == argScreen.uid) temp = s;
		if ( temp!= null){
		    Console.WriteLine("Already exists Uid {0}.", argScreen.uid);
		    return Created($"Already exists.", null);
		}else{
		    Console.WriteLine("Adding screen Uid {0}.", argScreen.uid);
		    ScreenList.Add(argScreen);
		    return Created($"Success, added Uid {argScreen.uid}.", null);
		}
	    }else{
		return Created($"Fail.", null);
	    }
	}
    }
}
