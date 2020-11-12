using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebServiceGilBT.Shared;
/* using System.Xml.Serialization; */
using System.IO;
/* using System.Text.Json; */
using System.Text;
using WebServiceGilBT.Services;

namespace WebServiceGilBT.Controller {
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ScreensController : ControllerBase {

        private SqlDataAccess _db;
        private SqlDataAccess db {
            get {
                if (_db == null) {
                    _db = new SqlDataAccess(null);
                }
                return _db;
            }
        }

        private IScreenListService _sls;
        public IScreenListService sls {
            get {
                if (_sls == null) {
                    _sls = new ScreenListMySQLService(db);
                }
                return _sls;
            }
        }

        [HttpGet]
        public IQueryable<Screen> GetScreenList() {
            ScreenList screenList = sls.GetGilBTScreenListAsync().Result;
            return screenList.Screens.AsQueryable();
        }

        private static Stream StringToStream(string src) {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        [HttpGet("{file_name}")]
        public Firmware GetFile(String file_name) {
            Debuger.PrintLn($"Getting file_name {file_name}.");
            Firmware temp = new Firmware($"Firmwares/{file_name}");
            return temp;
        }

        [HttpGet("{uid:int}")]
        public ScreenBin GetScreenBin(int uid) {
            Debuger.PrintLn($"Getting Binary representatin of screen {uid}");
            Screen _screen = sls.GetGilBTScreenAsync(uid).Result;
            if (_screen == null) {
                Debuger.PrintLn("GetScreen(): have found screen");
                _screen = new Screen { name = "null", uid = 0 };
            } else {
                Debuger.PrintLn("GetScreen(): have not found screen");
                sls.UpdateLastRequestTime(_screen);
            }
            ScreenBin retval = _screen.GetScreenBin();
            return retval;
        }

        [HttpGet("{uid:int}")]
        public Screen GetScreen(int uid) {
            Debuger.PrintLn($"Getting {uid}");
            Screen retval = sls.GetGilBTScreenAsync(uid).Result;
            if (retval == null) {
                Debuger.PrintLn("GetScreen(): have found screen");
                retval = new Screen { name = "null", uid = 0 };
            } else {
                Debuger.PrintLn("GetScreen(): have found screen");
                sls.UpdateLastRequestTime(retval);
            }
            return retval;
        }

        [HttpDelete("{uid:int}")]
        public IActionResult DeleteScreen(int uid) {
            return Created($"Deleted screen not supported", null);
        }

        [HttpPost]
        public IActionResult PostScreen([FromBody] Screen argScreen) {
            Debuger.PrintLn("Posting Screen {0}.", argScreen.uid);
            if (argScreen != null) {
                Screen temp = sls.GetGilBTScreenAsync(argScreen.uid).Result;
                if (temp != null) {
                    Debuger.PrintLn("Already exists Uid {0}.", argScreen.uid);
                    return Created($"Already exists.", null);
                } else {
                    Debuger.PrintLn("Adding screen Uid {0}.", argScreen.uid);
                    argScreen.from_led_screen = true;
                    sls.PostScreenAsync(argScreen);
                    return Created($"Success, added Uid {argScreen.uid}.", null);
                }
            } else {
                return Created($"Fail.", null);
            }
        }

        static public void CopyDataToDb() {
            IScreenListService sls = new ScreenListMySQLService(new SqlDataAccess(null));
            //ekrany
            List<Screen> ss = ScreenList.Load();
            foreach (Screen argScreen in ss) {
                Debuger.PrintLn("Posting Screen {0}.", argScreen.uid);
                if (argScreen != null) {
                    Screen temp = sls.GetGilBTScreenAsync(argScreen.uid).Result;
                    if (temp != null) {
                        Debuger.PrintLn("Already exists Uid {0}.", argScreen.uid);
                    } else {
                        Debuger.PrintLn("Adding screen Uid {0}.", argScreen.uid);
                        argScreen.from_led_screen = true;
                        sls.PostScreenAsync(argScreen);
                    }
                } else {
                }
            }
            //usery
            WebServiceGilBT.Services.UserMySQLService uls = new WebServiceGilBT.Services.UserMySQLService(new SqlDataAccess(null));
            foreach (WebServiceGilBT.Data.User user in UserList.users) {
                Console.WriteLine("adding: " + user.EmailAddress);
                if (user != null) {
                    WebServiceGilBT.Data.User userindb = uls.GetUserAsync(user.UserId).Result;
                    if (userindb == null) {
                        uls.AddUserAsync(user).Wait();
                    }
                }
            }
        }
    }
}
