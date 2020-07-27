using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebServiceGilBT.Shared;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;
using System.Text;

namespace WebServiceGilBT.Controller {
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ScreensController : ControllerBase {

        private static void LoadList() {
            if (_screenList == null) {
                _screenList = ScreenList.Load();
                foreach (Screen s in _screenList) {
                    /* s.firmware_ver = "kolejkowe_2020-06-10_7edce17"; */
                    /* s.firmware_ver = "kolejkowe_2020-06-10_7edce17"; */
                    /* s.firmware_ver = "kolejkowe_2020-06-09_50cf6f5"; */
                    s.firmware_ver = "NULL";
                }
            }
        }

        private static List<Screen> _screenList;

        public static List<Screen> screenList {
            get {
                LoadList();
                return _screenList;
            }
        }

        [HttpGet]
        public IQueryable<Screen> GetScreenList() {
            LoadList();
            return screenList.AsQueryable();
        }

        string pres_json = "{\"pages\":[{\"time\":5000, \"elements\":[]} ]}";

        private static Stream StringToStream(string src) {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        static int iter = 0;

        [HttpGet("{file_name}")]
        public Firmware GetFile(String file_name) {
            Debuger.PrintLn($"Getting file_name {file_name}.");
            Firmware temp = new Firmware($"Firmwares/{file_name}");
            return temp;
        }

        [HttpGet("{uid:int}")]
        public Screen GetScreen(int uid) {
            Debuger.PrintLn($"Getting {uid}");
            Screen temp = null;
            foreach (Screen s in screenList)
                if (s.uid == uid) temp = s;
            Screen retval = null;
            if (temp != null) {
                Debuger.PrintLn("GetScreen(): have found screen");
                retval = temp;
            } else {
                Debuger.PrintLn("GetScreen(): returning new screen");
                retval = new Screen { name = "null", uid = 0 };
            }
#if DEBUG
            retval.last_request = DateTime.Now;
#else
			//dodajemy 2 h dla serwera gdzies za granica
			retval.last_request = DateTime.Now.AddHours(2);
#endif
            return retval;
        }

        [HttpDelete("{uid:int}")]
        public IActionResult DeleteScreen(int uid) {
            Debuger.PrintLn("Trying to delete existing screen {0}.", uid);
            Screen temp = null;
            foreach (Screen s in screenList) if (s.uid == uid) temp = s;
            if (temp != null) {
                Debuger.PrintLn("Deleting existing screen {0}.", temp.uid);
                screenList.Remove(temp);
                ScreenList.Save(screenList);
                return Created($"Deleted screen", null);
            } else {
                return Created($"No such screen to delete.", null);
            }
        }

        [HttpPost]
        public IActionResult PostScreen([FromBody] Screen argScreen) {
            Debuger.PrintLn("Posting Screen {0}.", argScreen.uid);
            if (argScreen != null) {
                Screen temp = null;
                foreach (Screen s in screenList) {
                    Debuger.PrintLn(s.uid.ToString());
                    if (s.uid == argScreen.uid)
                        temp = s;
                }
                if (temp != null) {
                    Debuger.PrintLn("Already exists Uid {0}.", argScreen.uid);
                    if (argScreen.pres != null) {
                        if (argScreen.pres.ver != -1) {
                            Debuger.PrintLn("Post pochodzi z przegladarki");
                            screenList.Remove(temp);
                            screenList.Add(argScreen);
                            ScreenList.Save(screenList);
                        } else {
                            Debuger.PrintLn("post pochodzi od tablicy bo pres.ver==-1, więc czort z nim aktualizujemy go tylko gdy na serwerze nie ma danej tablicy");
                        }
                    }
                    return Created($"Already exists.", null);
                } else {
                    Debuger.PrintLn("Adding screen Uid {0}.", argScreen.uid);
#if DEBUG
                    argScreen.last_request = DateTime.Now;
#else
					argScreen.last_request = DateTime.Now.AddHours(2);
#endif
                    screenList.Add(argScreen);
                    ScreenList.Save(screenList);
                    return Created($"Success, added Uid {argScreen.uid}.", null);
                }
            } else {
                return Created($"Fail.", null);
            }
        }
    }
}
