using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServiceGilBT.Shared {
    public class HardCodedGilBTScreenListService :IScreenListService  {

	public ScreenList GetGilBTScreenList() {
	    return new ScreenList { 
		Screens = new List<Screen>{ 
		    new Screen() { uid= 0, name="Komorniki",},
			new Screen() { uid= 1, name="Kościan",},
			new Screen() { uid= 2, name="Lubon",},
			new Screen() { uid= 3, name="Grodzisk",},
			new Screen() { uid= 4, name="Łódź",},
			new Screen() { uid= 5, name="Stęszew",},
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
