using System.Threading.Tasks;

namespace WebServiceGilBT.Shared{
    public interface IScreenListService{
	public Task<ScreenList> GetGilBTScreenListAsync();
	public ScreenList GetGilBTScreenList();
    }
}
