using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Controller {
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ScreensController : ControllerBase {

        private static void LoadList() {
            if (_screenList == null) {
                _screenList = ScreenList.Load();
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

        static int iter = 0;
        [HttpGet("{uid:int}")]
        public JsonPage GetJsonPage(int uid) {
            Console.WriteLine($"Getting Json Page for {uid}");
            JsonPage p = new JsonPage();
            if (iter % 2 == 0) {
                Element e1 = new Element {
                    Color = 0x0000ff00,
                    Width = 0,
                    Height = 0,
                    Type = "line",
                    X = 0,
                    Y = 0,
                    Content = "Fuck you",
                    Fontsize = 12,
                    Fonttype = 0
                };
                Element e2 = new Element {
                    Color = 0x000000ff,
                    Width = 128,
                    Height = 32,
                    Type = "line",
                    X = 0,
                    Y = 8,
                    Content = "and fuck me",
                    Fontsize = 12,
                    Fonttype = 1
                };
                p.Elements = new List<Element>();
                p.Elements.Add(e1);
                p.Elements.Add(e2);
            } else {
                Element e1 = new Element {
                    Color = 0x0000ffff,
                    Width = 0,
                    Height = 0,
                    Type = "line",
                    X = 0,
                    Y = 0,
                    Content = "Love you",
                    Fontsize = 12,
                    Fonttype = 0
                };
                Element e2 = new Element {
                    Color = 0x00ff00ff,
                    Width = 128,
                    Height = 32,
                    Type = "line",
                    X = 0,
                    Y = 8,
                    Content = "and Love me",
                    Fontsize = 12,
                    Fonttype = 1
                };
                p.Elements = new List<Element>();
                p.Elements.Add(e1);
                p.Elements.Add(e2);
            }

            iter++;
            return p;
        }

        [HttpGet("{uid:int}")]
        public Firmware GetFirmware(int uid) {
            Console.WriteLine($"Getting firmware for {uid}");
            Firmware temp = new Firmware("rgb_cm4.frm");
            return temp;
        }

        [HttpGet("{uid:int}")]
        public Screen GetScreen(int uid) {
            Console.WriteLine($"Getting {uid}");
            Screen temp = null;
            foreach (Screen s in screenList) if (s.uid == uid) temp = s;
            if (temp != null) {
                temp.last_request = DateTime.Now;
                return temp;
            } else {
                return new Screen { name = "null", uid = 0 };
            }
        }

        [HttpPost]
        public IActionResult PostScreen([FromBody] Screen argScreen) {
            Console.WriteLine("Posting Screen {0}.", argScreen.uid);
            if (argScreen != null) {
                Screen temp = null;
                foreach (Screen s in screenList) if (s.uid == argScreen.uid) temp = s;
                if (temp != null) {
                    Console.WriteLine("Already exists Uid {0}.", argScreen.uid);
                    screenList.Remove(temp);
                    screenList.Add(argScreen);
                    ScreenList.Save(screenList);
                    return Created($"Already exists.", null);
                } else {
		    argScreen.last_request = DateTime.Now;
                    Console.WriteLine("Adding screen Uid {0}.", argScreen.uid);
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
