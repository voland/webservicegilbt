using System.Threading.Tasks;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Services {
    public interface IScreenListService {
        public Task DeleteScreenAsync(Screen argS);
        public void SetEditedScreen(Screen s);
        public Screen GetEditedScreen();
        public static Screen EditedScreen { set; get; }
        public Task PostScreenAsync(Screen argS);
        public Task<ScreenList> GetGilBTScreenListAsync();
        public ScreenList GetGilBTScreenList();
    }
}
