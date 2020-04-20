using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServiceGilBT.Shared {
    public class HardCodedGilBTScreenListService :IScreenListService  {


	public ScreenList GetGilBTScreenList() {
	    return new ScreenList { 
		Screens = new List<Screen>{ 
		    new Screen() { ID= 0, Name="Komorniki",},
			new Screen() { ID= 1, Name="Kościan",},
			new Screen() { ID= 2, Name="Lubon",},
			new Screen() { ID= 3, Name="Grodzisk",},
			new Screen() { ID= 4, Name="Łódź",},
			new Screen() { ID= 5, Name="Stęszew",},
		} 
	    };
	}

	public Task<ScreenList> GetGilBTScreenListAsync() {
	    return Task.FromResult<ScreenList>( 
		    GetGilBTScreenList()
		); 
	}
    }
}
