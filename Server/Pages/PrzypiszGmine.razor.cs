using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebServiceGilBT.Data;
using WebServiceGilBT.Services;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Pages {
    public partial class PrzypiszGmine : ComponentBase {
        public List<User> _userlist;

        [Inject]
        Blazored.SessionStorage.ISessionStorageService _sessionStorageService { set; get; }

        [Inject]
        UserMySQLService userService { set; get; }

		[Inject]
        ScreenListMySQLService scrLstSrvc {set; get;}

        protected async override Task OnInitializedAsync() {
            _userlist = await userService.GetUserListAsync();
            loggeduser = await GetLoggedUser();

        }

        protected async Task<User> GetLoggedUser() {
            string serializedUser = await _sessionStorageService.GetItemAsync<string>("loggedUser");
            User u = null;
            if (serializedUser != null)
                u = JsonSerializer.Deserialize<User>(serializedUser);
            if (u == null) {
                u = new User();
            }
            return u;
        }

        private User _u = null;
        public User loggeduser {
            set { _u = value; }
            get {
                if (_u == null) _u = new User();
                return _u;
            }
        }

        Screen ekran;

        [Parameter]
        public int Uid {
            set {
                ekran = scrLstSrvc.GetGilBTScreenAsync(value).Result;
            }
        }

    }
}
