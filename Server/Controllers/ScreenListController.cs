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

        [HttpGet("{uid:int}")]
        public ApiPres GetApiPres(int uid) {
            Console.WriteLine($"Getting Json Presentation for {uid}");
            ApiPres ap = new ApiPres();
            ApiPage page1 = new ApiPage(5000);
            page1.elements.Add(ApiPageElement.NewApiText($"Love {DateTime.Now}", 32, 16, 0xffffffff, FontNames.fontnormal));
            page1.elements.Add(ApiPageElement.NewApiText("Forever", 32, 24, 0xffffffff, FontNames.fontfat));
            ApiPage page2 = new ApiPage(5000);
            page2.elements.Add(ApiPageElement.NewApiText("Hate never", 32, 16, 0xffffffff, FontNames.fontnormal));
            page2.elements.Add(ApiPageElement.NewApiText($"Uid {uid}", 32, 24, 0xffffffff, FontNames.fontfat));
            ap.pages.Add(page1);
            ap.pages.Add(page2);
            return ap;
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

        [HttpGet("{file_name}")]
        public Firmware GetFile(String file_name) {
            Console.WriteLine($"Getting file_name {file_name}.");
            Firmware temp = new Firmware($"Firmwares/{file_name}");
            return temp;
        }

        [HttpGet("{uid:int}")]
        public Screen GetScreen(int uid) {
            Console.WriteLine($"Getting {uid}");
            Screen temp = null;
            foreach (Screen s in screenList) if (s.uid == uid) temp = s;
            if (temp != null) {
                temp.last_request = DateTime.Now;
                //temp data
                return temp;
            } else {
                return new Screen { name = "null", uid = 0 };
            }
        }

        [HttpDelete("{uid:int}")]
        public IActionResult DeleteScreen(int uid) {
            Console.WriteLine("Trying to delete existing screen {0}.", uid);
            Screen temp = null;
            foreach (Screen s in screenList) if (s.uid == uid) temp = s;
            if (temp != null) {
                Console.WriteLine("Deleting existing screen {0}.", temp.uid);
                screenList.Remove(temp);
                ScreenList.Save(screenList);
                return Created($"Deleted screen", null);
            } else {
                return Created($"No such screen to delete.", null);
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
