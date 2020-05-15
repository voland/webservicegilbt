using System.Threading.Tasks;

namespace WebServiceGilBT.Shared {
    public interface IScreenListService {
	public Task DeleteScreenAsync(Screen argS);
        public Task PostScreenAsync(Screen argS);
        public Task<ScreenList> GetGilBTScreenListAsync();
        public ScreenList GetGilBTScreenList();
    }
}
