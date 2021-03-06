using System;
using WebServiceGilBT.Data;
using System.Collections.Generic;

namespace WebServiceGilBT.Shared {
    public class Screen : IScreen {
        public int uid { set; get; }
        public string name { set; get; }
        public string firmware_ver { set; get; }
        /* public byte[] firmware_bin { set; get; } */
        public int contrast { set; get; }
        public int contrast_night { set; get; }

        private int _contrast_max;
        public int contrast_max {
            set { _contrast_max = value; }
            get {
                if (screen_type == eScreenType.mono) return 10;
                return _contrast_max;
            }
        }

        public DateTime last_request { set; get; }
        public eScreenType screen_type { set; get; }
        public bool from_led_screen { set; get; }

        private int _width;

        public int width {
            set {
                //tylko wielokrotnosc 32
                value /= 32;
                value *= 32;
                if (value > 512) {
                    value = 512;
                }
                _width = value;
            }
            get {
                return _width;
            }
        }

        private int _height;

        public int height {
            set {
                //tylko wielokrotnosc 32
                value /= 16;
                value *= 16;
                if (value > 256) {
                    value = 256;
                }
                _height = value;
            }
            get {
                return _height;
            }
        }

        public bool dhcp { set; get; }
        public string ip { set; get; }
        public string ma { set; get; }
        public string gw { set; get; }
        public int IdGminy;
        public Gmina gmina = new Gmina();

        private Pres _pres = null;
        public void InitPres() {
            Debuger.PrintLn("_pres of {0} {1} is null its reininitalasing", uid, name);
            _pres = new Pres();

            Page p1 = new Page(5);
            p1.elements.Add(PageElement.NewText("Screen uid:", 0, 0, 1, FontType.fontnormal8px));
            p1.elements.Add(PageElement.NewText(uid.ToString(), 0, 8, 1, FontType.fontnormal8px));
            _pres.pages.Add(p1);

            Page p2 = new Page(5);
            p2.elements.Add(PageElement.NewText("Time Now:", 0, 0, 1, FontType.fontnormal8px));
            p2.elements.Add(PageElement.NewTime(0, 8, 1, FontType.fontnormal8px));
            p2.elements.Add(PageElement.NewDate(32, 8, 1, FontType.fontnormal8px));
            _pres.pages.Add(p2);

            Page p3 = new Page(5);
            p3.elements.Add(PageElement.NewText("Mikołów pm2,5:", 0, 0, 1, FontType.fontnormal8px));
            p3.elements.Add(PageElement.NewSensorPm2_5(444, 0, 8, 1, FontType.fontnormal8px));
            _pres.pages.Add(p3);

            Page p4 = new Page(5);
            p4.elements.Add(PageElement.NewText("Mikołów pm10:", 0, 0, 1, FontType.fontnormal8px));
            p4.elements.Add(PageElement.NewSensorPm10(444, 0, 8, 1, FontType.fontnormal8px));
            _pres.pages.Add(p4);
        }

        public Pres pres {
            set {
                _pres = value;
                foreach (Page p in _pres.pages) {
                    foreach (PageElement pe in p.elements) {
                        pe.set_uid_for_preview(uid.ToString());
                    }
                }
            }
            get {
                if (_pres == null) InitPres();
                return _pres;
            }
        }

        public Screen() {
            from_led_screen = false;
            ActualiseLastRequestTime();
        }

        public string resolution() {
            return $"{width}x{height}";
        }

        public string contrast_str() {
            return $"{contrast}/{contrast_max}";
        }

        public string night_contrast_str() {
            return $"{contrast_night}/{contrast_max}";
        }

        public string screen_type_str() {
            switch (screen_type) {
                case eScreenType.rgb: return "Rgb";
                case eScreenType.mono: return "Mono";
                case eScreenType.unknown: return "Unknown";
            }
            return "unknonw";
        }

        public void ActualiseLastRequestTime() {
            last_request = MyClock.Now;
        }

        public ScreenBin GetScreenBin() {
            ScreenBin sc = new ScreenBin();
            sc.uid = this.uid;
            sc.name = this.name;
            sc.firmware_ver = this.firmware_ver;
            sc.contrast = this.contrast;
            sc.contrast_night = this.contrast_night;
            sc.contrast_max = sc.contrast_max;
            sc.height = this.height;
            sc.width = this.width;
            sc.dhcp = this.dhcp;
            sc.ip = this.ip;
            sc.gw = this.gw;
            sc.ma = this.ma;
            if (this.pres != null)
                sc.pres = this.pres.GetPresBin().ToByteArray();
            return sc;
        }
    }
}
