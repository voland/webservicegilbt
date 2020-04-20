using System.Threading.Tasks;

namespace WebServiceGilBT.Shared{
    public interface IScreenListService{
	public Task<GilBTScreenList> GetGilBTScreenList();
    }
}
