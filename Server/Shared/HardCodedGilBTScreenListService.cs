using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServiceGilBT.Shared {
    public class HardCodedGilBTScreenListService :IScreenListService  {

	public ScreenList GetGilBTScreenList() {
	    return new ScreenList { 
		Screens = new List<Screen>{ 
		    new Screen() { UID= 0, Name="Komorniki",},
			new Screen() { UID= 1, Name="Kościan",},
			new Screen() { UID= 2, Name="Lubon",},
			new Screen() { UID= 3, Name="Grodzisk",},
			new Screen() { UID= 4, Name="Łódź",},
			new Screen() { UID= 5, Name="Stęszew",},
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
